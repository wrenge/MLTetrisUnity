using System;
using System.Collections.Generic;

[Serializable]
public struct MoveInfo
{
	public int MoveNumber;
	public int AbsoluteMoveNumber;
	public List<int> Input;
	public int Output;

	public MoveInfo(TetrisModel model, TetrisMove move)
	{
		Output = (int) move;
		
		var inputResult = model.GetFieldSnapshot();

		Input = inputResult.Data;
		MoveNumber = model.MoveCount;
		AbsoluteMoveNumber = model.TotalMoveCount;
	}

	public TetrisMove GetMove() => (TetrisMove) Output;
}