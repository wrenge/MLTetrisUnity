using System;
using UnityEngine;

public class PlayerGame : MonoBehaviour
{
	[SerializeField] private uint _seed = 2;
	
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
	}

	private void Update()
	{
		_controller.Update();
	}
}