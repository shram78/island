using UnityEngine;
using DG.Tweening;

public class ObserverZoneRaft : MonoBehaviour
{
    [SerializeField] private Zone _zoneBranch;
    [SerializeField] private Zone _zoneLog;
    [SerializeField] private Zone _zoneBarell;
    [SerializeField] private GameObject _raftRoot;
    [SerializeField] private GameObject _raftAlfa;
    [SerializeField] private LevelComlete _levelComplete;

    [SerializeField] private float _heightToUp;
    [SerializeField] private float _timeToUp = 0.5f;

    [SerializeField] private GameObject[] _barrelsInRaft;

    private int _currenBarrel = 0;

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

    private void Update()
    {
        _currenBarrel = _zoneBarell.CountPalm;
        _barrelsInRaft[_currenBarrel].gameObject.SetActive(true);
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
        Delivery();
    }

    private void SpawnRaft()
    {
        if (_isBranchOpen && _isLogOpen)
        {
            _raftRoot.gameObject.SetActive(true);
            _raftAlfa.gameObject.SetActive(false);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_raftRoot.transform.DOLocalMoveY(_heightToUp, _timeToUp));
            sequence.Insert(_timeToUp, _raftRoot.transform.DOShakeScale(0.3f, 0.1f, 5));
        }
    }

    private void Delivery()
    {
        if (_isBarellOpen)
            _levelComplete.SetFoofCollected();
    }
}
