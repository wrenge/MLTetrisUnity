using System;
using UnityEngine;

public class PlayerGame : MonoBehaviour
{
	public bool IsPaused;
	
	[SerializeField] private uint _seed = 2;
	[SerializeField] private float _dropPeriod = 1;
	
	private TetrisModel _model;
	private TetrisView _view;
	private PlayerTetrisController _controller;

	private void Awake()
	{
		_view = FindObjectOfType<TetrisView>();
		if (_view == null)
			throw new Exception("Can't find TetrisView in scene");
		_model = new TetrisModel(seed: _seed);
		_controller = new PlayerTetrisController();
	}

	private void Start()
	{
		_view.Init(_model);
		_controller.Init(_model);
		_controller.FigureDropPeriod = _dropPeriod;
	}

	private void Update()
	{
		if(IsPaused)
			_controller.Update(Time.deltaTime);
	}

	private void OnDestroy()
	{
		_controller.SaveSession();
	}
}