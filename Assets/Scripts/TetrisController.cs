using System;
using Unity.Mathematics;

public class TetrisController
{
	protected TetrisModel Model;

	public void Init(TetrisModel model)
	{
		Model = model;
		Reset();
	}

	public void Update()
	{
		if(Model == null)
			return;

		OnUpdate();
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
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(move), move, null);
		}
	}

	protected virtual void Reset()
	{
		Model.Seed++;
		Model.Reset();
	}

	protected virtual void OnUpdate()
	{
	}
}