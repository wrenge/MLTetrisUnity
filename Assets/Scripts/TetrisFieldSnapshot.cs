using System;
using System.Collections.Generic;

public struct TetrisFieldSnapshot
{
	public List<int> Data;

	public TetrisFieldSnapshot ToBlackWhite()
	{
		for (var i = 0; i < Data.Count; i++)
		{
			if (Data[i] != 0)
				Data[i] /= Math.Abs(Data[i]);
		}

		return this;
	}

	public TetrisFieldSnapshot ToFlat()
	{
		for (var i = 0; i < Data.Count; i++)
		{
			Data[i] = Math.Abs(Data[i]);
		}

		return this;
	}
}