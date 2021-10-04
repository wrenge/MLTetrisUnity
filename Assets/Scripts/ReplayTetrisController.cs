using System.Collections.Generic;

public class ReplayTetrisController : TetrisController
{
	private readonly List<MoveInfo> _moves;
	private int _counter = 0;

	public ReplayTetrisController(List<MoveInfo> moves)
	{
		_moves = moves;
	}

	protected override void OnUpdate()
	{
		if(_counter >= _moves.Count)
			return;
		
		var move = _moves[_counter].GetMove();
		MakeMove(move);
		_counter++;
	}

	protected override void Reset()
	{
		base.Reset();
		_counter = 0;
	}
}