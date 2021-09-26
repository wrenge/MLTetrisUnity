using Unity.Mathematics;

public static class MathExtensions
{
	public static bool Contains(this int4 vec, int obj)
	{
		for (int i = 0; i < 4; i++)
		{
			if (vec[i] == obj)
				return true;
		}

		return false;
	}
}