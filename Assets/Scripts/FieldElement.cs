using UnityEngine;

public struct FieldElement
{
	public bool Occupied;
	public int Color;
	public bool IsFigure;

	public FieldElement(bool occupied = false, int color = -1, bool isFigure = false)
	{
		Occupied = occupied;
		Color = color;
		IsFigure = isFigure;
	}
}