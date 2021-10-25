using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine.InputSystem;

public class FullTetrisAgent : Agent, ITetrisAgent
{
	public int TotalMoveCount { get; set; }
	public int CurrentMoveCount { get; set; }
	public TetrisFieldSnapshot CurrentFieldObservation { get; set; }
	public bool IsRecording { get; private set; }
	public MLTetrisController TetrisController { get; set; }

	[SerializeField] private int _rewardPerClear = 2;
	[SerializeField] private int _punishmentPerDrop = -1;
	[SerializeField] private InputAction _left;
	[SerializeField] private InputAction _right;
	[SerializeField] private InputAction _jump;
	[SerializeField] private InputAction _rotate;
	[SerializeField] private InputAction _down;

	private TetrisMove _userMove;
	[SerializeField] private float _punishmentPerFail = -20;

	private void Awake()
	{
		IsRecording = GetComponent<BehaviorParameters>().BehaviorType == BehaviorType.HeuristicOnly;
		_left.performed += context => OnUserPerformedMoveHandler(TetrisMove.Left);
		_right.performed += context => OnUserPerformedMoveHandler(TetrisMove.Right);
		_down.performed += context => OnUserPerformedMoveHandler(TetrisMove.Down);
		_jump.performed += context => OnUserPerformedMoveHandler(TetrisMove.Jump);
		_rotate.performed += context => OnUserPerformedMoveHandler(TetrisMove.Rotate);
		_left.Enable();
		_right.Enable();
		_down.Enable();
		_jump.Enable();
		_rotate.Enable();
	}

	private void OnUserPerformedMoveHandler(TetrisMove move)
	{
		_userMove = move;
		RequestDecision();
		RequestAction();
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		var snapshot = CurrentFieldObservation.ToBlackWhite();
		foreach (var unit in snapshot.Data)
		{
			sensor.AddObservation(unit);
		}

		sensor.AddObservation(TotalMoveCount);
		sensor.AddObservation(CurrentMoveCount);
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		var move = (TetrisMove) (actions.DiscreteActions[0] - 1);
		TetrisController.RequestMove(move);
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var discreteActions = actionsOut.DiscreteActions;
		discreteActions[0] = (int) _userMove + 1;
	}

	public void OnClearRow()
	{
		AddReward(_rewardPerClear);
	}

	public void OnDropFigure()
	{
		AddReward(_punishmentPerDrop);
	}

	public void Stop()
	{
		EndEpisode();
	}

	public void OnFailGame()
	{
		AddReward(_punishmentPerFail);
	}
}