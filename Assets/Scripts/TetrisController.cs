using System;
using Unity.Mathematics;
using UnityEngine;

public class TetrisController
{
	protected TetrisModel Model;
	public bool IsPaused { get; set; }
	public const string SessionSavePath = "Sessions/Session_{0}.json";

	public void Init(TetrisModel model)
	{
		Model = model;
		Reset();
	}

	public void Update(float deltaTime)
	{
		if(Model == null || !Model.CanPlay || IsPaused)
			return;

		OnUpdate(deltaTime);
	}

	protected virtual void MakeMove(TetrisMove move)
	{
		switch (move)
		{
			case TetrisMove.Ignore:
				Model.MoveFigure(new int2(0, 1));
				break;
			case TetrisMove.Rotate:
				Model.RotateFigure();
				break;
			case TetrisMove.Left:
				Model.MoveFigure(new int2(-1, 0));
				break;
			case TetrisMove.Down:
				Model.MoveFigure(new int2(0, 1));
				break;
			case TetrisMove.Right:
				Model.MoveFigure(new int2(1, 0));
				break;
			case TetrisMove.Jump:
				Model.DropFigure();
				break;
			case TetrisMove.Reset:
				Reset();
				break;
			case TetrisMove.Quit:
				Application.Quit();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(move), move, null);
		}
	}

	public virtual void Reset()
	{
		Model.Reset();
	}

	protected virtual void OnUpdate(float deltaTime)
	{
	}
}