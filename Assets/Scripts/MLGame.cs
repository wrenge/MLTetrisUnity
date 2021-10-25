using System;
using Unity.MLAgents;
using UnityEngine;

public class MLGame : MonoBehaviour
{
	[SerializeField] private uint _seed = 2;
	[SerializeField] private float _dropPeriod = 1;
	[SerializeField] private TetrisView _view;
	[SerializeField] private Agent _agentReference;

	private ITetrisAgent _agent;
	private TetrisModel _model;
	private MLTetrisController _controller;
	private int _maxHeight;

	private void Awake()
	{
		_agent = _agentReference.GetComponent<ITetrisAgent>();
		if (_view == null)
			throw new Exception("Can't find TetrisView in scene");
		if (_agent == null)
			throw new Exception("Can't find TetrisAgent in scene");
		_model = new TetrisModel(seed: _seed);
		_controller = new MLTetrisController();
	}

	private void Start()
	{
		_model.Seed++;
		_view.Init(_model);
		_controller.Init(_model);
		_controller.FigureDropPeriod = _dropPeriod;
		_controller.OnMoveMade += ControllerOnMoveMadeHandler;
		_agent.TetrisController = _controller;
		// _model.OnFigureSet += ModelOnFigureSetHandler;
		_model.OnNewFigureCreated += ModelOnNewFigureCreatedHandler;
		_model.OnLineCleared += ModelOnLineClearedHandler;
		_model.OnGameOver += ModelOnGameOverHandler;
	}

	private void ModelOnNewFigureCreatedHandler()
	{
		var newMaxHeight = GetMaxHeight();
		var reward = -(newMaxHeight - _maxHeight);
		_agent.AddReward(reward);
		Debug.Log($"Reward: {reward}");
		_maxHeight = newMaxHeight;
	}

	private int GetMaxHeight()
	{
		for (int y = 0; y < _model.Dimensions.y; y++)
		{
			for (int x = 0; x < _model.Dimensions.x; x++)
			{
				if (_model.Field[x, y].Occupied)
					return _model.Dimensions.y - y;
			}
		}

		return 0;
	}

	private void ModelOnGameOverHandler()
	{
		_agent.OnFailGame();
		_agent.Stop();
		_controller.Reset();
		_maxHeight = 0;
	}

	private void ModelOnLineClearedHandler()
	{
		_agent.OnClearRow();
		Debug.LogWarning("Line cleared!");
	}

	private void ModelOnFigureSetHandler()
	{
		_agent.OnDropFigure();
	}

	private void ControllerOnMoveMadeHandler(TetrisMove obj)
	{
		// _agent.RequestDecision();
		// _agent.RequestAction();
	}

	private void Update()
	{
		if (!_model.CanPlay)
			return;

		_agent.CurrentFieldObservation = _model.GetFieldSnapshot();
		_agent.CurrentMoveCount = _model.MoveCount;
		_agent.TotalMoveCount = _model.TotalMoveCount;
		
		if(!_agent.IsRecording)
		{
			_agent.RequestDecision();
			_agent.RequestAction();
		}
		
		_controller.Update(Time.deltaTime);
	}

	public void SetPaused(bool value) => _controller.IsPaused = value;
	public void TogglePause() => _controller.IsPaused = !_controller.IsPaused;

	private void OnDestroy()
	{
		_agent?.Stop();
	}
}