using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObserverZoneRaft : MonoBehaviour
{
    [SerializeField] private Zone _zoneBranch;
    [SerializeField] private Zone _zoneLog;
    [SerializeField] private Zone _zoneBarell;
    [SerializeField] private GameObject _raftRoot;
    [SerializeField] private GameObject _raftAlfa;

    [SerializeField] private float _heightToUp;
    [SerializeField] private float _timeToUp = 0.5f;

    private bool _isBranchOpen = false;
    private bool _isLogOpen = false;
    private bool _isBarellOpen = false;

    private void OnEnable()
    {
        _zoneBranch.Opened += OnSetBranch;
        _zoneLog.Opened += OnSetLog;
        _zoneBarell.Opened += OnSetBarell;
    }

    private void OnDisable()
    {
        _zoneBranch.Opened -= OnSetBranch;
        _zoneLog.Opened -= OnSetLog;
        _zoneBarell.Opened -= OnSetBarell;
    }

    private void OnSetBranch()
    {
        _isBranchOpen = true;
        SpawnRaft();
    }
    private void OnSetLog()
    {
        _isLogOpen = true;
        SpawnRaft();
    }
    private void OnSetBarell()
    {
        _isBarellOpen = true;
        SpawnRaft();
    }

    private void SpawnRaft()
    {
        if (_isBranchOpen && _isLogOpen && _isBarellOpen)
        {
            _raftRoot.gameObject.SetActive(true);
            _raftAlfa.gameObject.SetActive(false);

            _raftRoot.transform.DOLocalMoveY(_heightToUp, _timeToUp);

            _raftRoot.transform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.4f);
        }
    }
}
