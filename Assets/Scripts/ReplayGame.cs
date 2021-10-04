using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReplayGame : MonoBehaviour
{
    [SerializeField] private int _sessionNumber;
    [SerializeField] private float _updatePeriod = 0.5f;

    private float _nextUpdateTime;
    private TetrisModel _model;
    private TetrisView _view;
    private ReplayTetrisController _controller;
    private SessionInfo _sessionInfo;

    private void Awake()
    {
        _view = FindObjectOfType<TetrisView>();
        if (_view == null)
            throw new Exception("Can't find TetrisView in scene");

        
        var sessionSavePath = TetrisController.SessionSavePath;
        using var file = File.OpenText(string.Format(sessionSavePath, _sessionNumber));
        var sessionJson = file.ReadToEnd();
        _sessionInfo = JsonUtility.FromJson<SessionInfo>(sessionJson);
        
        _model = new TetrisModel(seed: _sessionInfo.Seed);
        _controller = new ReplayTetrisController(_sessionInfo.Moves);
    }

    private void Start()
    {
        _view.Init(_model);
        _controller.Init(_model);
    }

    private void Update()
    {
        if(_nextUpdateTime > Time.time)
            return;

        _controller.Update();
        _nextUpdateTime = Time.time + _updatePeriod;
    }
}
