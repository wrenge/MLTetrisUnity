public interface ITetrisAgent
{
	int TotalMoveCount { set; }
	int CurrentMoveCount { set; }
	TetrisFieldSnapshot CurrentFieldObservation { set; }
	bool IsRecording { get; }
	MLTetrisController TetrisController { get; set; }

	void RequestDecision();
	void RequestAction();
	void OnClearRow();
	void OnDropFigure();
	void Stop();
	void AddReward(float value);
	void OnFailGame();
}