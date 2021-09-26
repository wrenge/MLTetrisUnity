using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerTetrisController
{
	private TetrisModel _model;

	public void Init(TetrisModel model)
	{
		_model = model;
	}

	public void Update()
	{
		if(_model == null)
			return;

		if (Input.GetKeyDown(KeyCode.DownArrow))
			_model.MoveFigure(new int2(0, 1));
		if (Input.GetKeyDown(KeyCode.RightArrow))
			_model.MoveFigure(new int2(1, 0));
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			_model.MoveFigure(new int2(-1, 0));
		if (Input.GetKeyDown(KeyCode.UpArrow))
			_model.RotateFigure();
		if (Input.GetKeyDown(KeyCode.Space))
			_model.DropFigure();
	}
}