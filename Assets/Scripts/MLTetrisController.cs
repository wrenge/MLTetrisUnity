using System;
using System.IO;
using UnityEngine;

public class MLTetrisController : TetrisController
{
	public event Action<TetrisMove> OnMoveMade; 
	public float FigureDropPeriod = 1;
	private float _nextDropCounter = 0;

	public override void Reset()
	{
		_nextDropCounter = FigureDropPeriod;
		base.Reset();
	}

	protected override void OnUpdate(float deltaTime)
	{
		if (_nextDropCounter <= 0)
			MakeMove(TetrisMove.Ignore);
		
		_nextDropCounter -= deltaTime;
	}

	public void RequestMove(TetrisMove move)
	{
		if(move == TetrisMove.Ignore)
			return;
		
		MakeMove(move);
	}

	protected override void MakeMove(TetrisMove move)
	{
		if (move == TetrisMove.Down || move == TetrisMove.Jump || move == TetrisMove.Ignore)
			_nextDropCounter = FigureDropPeriod;
		
		base.MakeMove(move);
		OnMoveMade?.Invoke(move);
	}
}