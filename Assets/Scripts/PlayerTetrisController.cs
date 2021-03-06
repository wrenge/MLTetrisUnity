using System;
using System.IO;
using UnityEngine;

public class PlayerTetrisController : TetrisController
{
	public float FigureDropPeriod = 1;
	private const string SessionNumberKey = "Sessions";
	private SessionInfo _sessionInfo;
	private float _nextDropCounter = 0;

	public override void Reset()
	{
		Model.Seed++;
		base.Reset();

		if (_sessionInfo != null)
			SaveSession();

		int sessionNumber = PlayerPrefs.GetInt(SessionNumberKey, 0) + 1;
		_sessionInfo = new SessionInfo
		{
			SessionNumber = sessionNumber,
			Dimensions = Model.Dimensions,
			Seed = Model.Seed
		};
		_nextDropCounter = FigureDropPeriod;
	}

	protected override void OnUpdate(float deltaTime)
	{
		if(Model.CanPlay)
		{
			if (Input.GetKeyDown(KeyCode.DownArrow))
				MakeMove(TetrisMove.Down);
			if (Input.GetKeyDown(KeyCode.RightArrow))
				MakeMove(TetrisMove.Right);
			if (Input.GetKeyDown(KeyCode.LeftArrow))
				MakeMove(TetrisMove.Left);
			if (Input.GetKeyDown(KeyCode.UpArrow))
				MakeMove(TetrisMove.Rotate);
			if (Input.GetKeyDown(KeyCode.Space))
				MakeMove(TetrisMove.Jump);
			if (_nextDropCounter <= 0)
				MakeMove(TetrisMove.Ignore);
			_nextDropCounter -= deltaTime;
		}
	}

	protected override void MakeMove(TetrisMove move)
	{
		if(move != TetrisMove.Reset && move != TetrisMove.Quit)
			_sessionInfo.Moves.Add(new MoveInfo(Model, move));
		if (move == TetrisMove.Down || move == TetrisMove.Jump || move == TetrisMove.Ignore)
			_nextDropCounter = FigureDropPeriod;

		base.MakeMove(move);
	}

	public void SaveSession()
	{
		if (_sessionInfo == null)
			return;

		PlayerPrefs.SetInt(SessionNumberKey, _sessionInfo.SessionNumber);
		PlayerPrefs.Save();
		string sessionJson = JsonUtility.ToJson(_sessionInfo);
		Directory.CreateDirectory("Sessions");
		using var file = File.CreateText(string.Format(SessionSavePath, _sessionInfo.SessionNumber));
		file.Write(sessionJson);
	}
}