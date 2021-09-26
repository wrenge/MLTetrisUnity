using Unity.Mathematics;
using UnityEngine;

public struct Figure
{
	public int2 Position;
	public int Color { get; }
	private readonly int _type;
	private int _rotation;

	public Figure(int2 position, int maxColors, int randInt)
	{
		Position = position;
		Color = math.abs(randInt) % maxColors;
		_type = math.abs(randInt) % Figures.Length;
		_rotation = 0;
	}

	private static readonly int4[][] Figures =
	{
		new[] {new int4(1, 5, 9, 13), new int4(4, 5, 6, 7)},
		new[] {new int4(4, 5, 9, 10), new int4(2, 6, 5, 9)},
		new[] {new int4(6, 7, 9, 10), new int4(1, 5, 6, 10)},
		new[] {new int4(1, 2, 5, 9), new int4(0, 4, 5, 6), new int4(1, 5, 9, 8), new int4(4, 5, 6, 10)},
		new[] {new int4(1, 2, 6, 10), new int4(5, 6, 7, 9), new int4(2, 6, 10, 11), new int4(3, 5, 6, 7)},
		new[] {new int4(1, 4, 5, 6), new int4(1, 4, 5, 9), new int4(4, 5, 6, 9), new int4(1, 5, 6, 9)},
		new[] {new int4(1, 2, 5, 6)},
	};

	public int4 GetImage()
	{
		return Figures[_type][_rotation];
	}

	public void Rotate(int i = 1)
	{
		_rotation = (_rotation + i) % Figures[_type].Length;
		if (_rotation < 0)
			_rotation = Figures[_type].Length - 1;
	}
}