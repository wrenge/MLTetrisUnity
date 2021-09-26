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
		Input = new List<int>(model.Dimensions.x * model.Dimensions.y);
		var viewData = model.GetViewData();
		for (int y = 0; y < model.Dimensions.y; y++)
		{
			for (int x = 0; x < model.Dimensions.x; x++)
			{
				var element = viewData[x, y];
				int inputElement = 0;
				if (element.Occupied) 
					inputElement = element.IsFigure ? -(element.Color + 1) : element.Color + 1;
				Input.Add(inputElement);
			}
		}
		MoveNumber = model.MoveCount;
		AbsoluteMoveNumber = model.TotalMoveCount;
	}
}