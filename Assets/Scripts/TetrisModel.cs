using System;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public class TetrisModel
{
	public int2 Dimensions { get; }
	public FieldElement[,] Field { get; private set; }
	public uint Seed { get; set; }
	public bool CanPlay { get; private set; }
	public int MaxColors { get; set; }
	public int TotalMoveCount { get; private set; }
	public int MoveCount { get; private set; }

	private Random _random;
	private readonly FieldElement[,] _viewBuffer;
	private Figure _figure;

	public TetrisModel(int width = 10, int height = 20, uint seed = 1)
	{
		Dimensions = new int2(width, height);
		Field = new FieldElement[Dimensions.x, Dimensions.y];
		_viewBuffer = new FieldElement[Dimensions.x, Dimensions.y];
		MaxColors = 6;
		Seed = seed;
		Reset();
	}

	public void Reset()
	{
		_random = new Random(Seed);
		TotalMoveCount = MoveCount = 0;

		for (int y = 0; y < Dimensions.y; y++)
		{
			ClearRow(y);
		}
		
		CreateNewFigure();
		CanPlay = true;
	}

	public void FreezeFigure()
	{
		MergeFigure(Field);
		BreakLines();
		CreateNewFigure();
		CanPlay = !IsFigureOverlapping();
		MoveCount = 0;
	}

	private void CreateNewFigure()
	{
		_figure = new Figure(new int2(3, 0), MaxColors, _random.NextInt());
	}

	public void MergeFigure(FieldElement[,] field, bool keepFigure = false)
	{
		int4 image = _figure.GetImage();
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (!image.Contains(i * 4 + j))
					continue;

				field[j + _figure.Position.x, i + _figure.Position.y] = new FieldElement(true, _figure.Color, keepFigure);
			}
		}
	}

	public void BreakLines()
	{
		for (int y = Dimensions.y - 1; y >= 0; y--)
		{
			if (IsRowFull(y))
			{
				for (int y1 = y; y1 >= 1; y1--)
				{
					CopyRow(y1 - 1, y1);
				}
			}
		}
	}

	private bool IsRowFull(int y)
	{
		for (int x = 0; x < Dimensions.x; x++)
		{
			if (!Field[x, y].Occupied)
				return false;
		}

		return true;
	}

	private void ClearRow(int y)
	{
		for (int x = 0; x < Dimensions.x; x++)
		{
			Field[x, y] = default;
		}
	}

	private void CopyRow(int fromY, int toY)
	{
		for (int x = 0; x < Dimensions.x; x++)
		{
			Field[x, toY] = Field[x, fromY];
		}
	}

	public bool IsFigureOverlapping()
	{
		int4 image = _figure.GetImage();
		
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (!image.Contains(i * 4 + j)) continue;
				if (
					i + _figure.Position.y > this.Dimensions.y - 1
					|| j + _figure.Position.x > this.Dimensions.x - 1
					|| j + _figure.Position.x < 0
					|| Field[j + _figure.Position.x, i + _figure.Position.y].Occupied
				)
					return true;
			}
		}
		
		return false;
	}

	public FieldElement[,] GetViewData()
	{
		Array.Copy(Field, _viewBuffer, Field.Length);
		MergeFigure(_viewBuffer, true);
		
		return _viewBuffer;
	}

	public bool MoveFigure(int2 dir, bool countMove = true)
	{
		var oldPos = _figure.Position;
		_figure.Position += dir;
		
		if (!IsFigureOverlapping())
		{
			if (countMove)
				IncMoves();
			return true;
		}
		
		_figure.Position = oldPos;
		if (dir.Equals(new int2(0, 1))) 
			FreezeFigure();
		return false;
	}

	public void DropFigure()
	{
		while (MoveFigure(new int2(0, 1), false))
		{
		}
		IncMoves();
	}

	public void RotateFigure()
	{
		_figure.Rotate();
		if(IsFigureOverlapping())
			_figure.Rotate(-1);
		else
			IncMoves();
	}

	private void IncMoves()
	{
		MoveCount++;
		TotalMoveCount++;
	}
}