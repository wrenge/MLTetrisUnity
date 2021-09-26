using System;
using System.Collections.Generic;
using Unity.Mathematics;

[Serializable]
public class SessionInfo
{
	[NonSerialized] public int SessionNumber;
	public uint Seed;
	public int2 Dimensions;
	public List<MoveInfo> Moves = new List<MoveInfo>();
}