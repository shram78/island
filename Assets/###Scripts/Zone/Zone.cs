using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;

public class Zone : MonoBehaviour
{
    [SerializeField] private bool _isBranch;
    [SerializeField] private bool _isLog;
    [SerializeField] private bool _isBarrel;
    [SerializeField] private bool _isBanana;

    [SerializeField] private Transform _pointToMove;
    [SerializeField] private TMP_Text _numberBranch;
    [SerializeField] private int _countBranch;

    [SerializeField] private bool _isLogSpawner;
    [SerializeField] private LogSpawner _logSpawner;

    private int _currentNumBranch = 0;
    private int _maxBranch;

    public UnityAction Opened;
    public UnityAction<int> PrefabMoveStock;
    public UnityAction<int> Test;


    public int CountPalm => _countBranch;

    private void Start()
    {
        if (_numberBranch != null)
        {
            _maxBranch = _countBranch;
            ChangedCounter();
        }
    }

    private void OnEnable()
    {
        if (_numberBranch != null)
        ChangedCounter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bag))
        {
            if (_isBranch)
                bag.DropBranch(_countBranch, _pointToMove, this);
            if (_isLog)
                bag.DropLog(_countBranch, _pointToMove, this);
            if (_isBarrel)
                bag.DropBarell(_countBranch, _pointToMove, this);
            if (_isBanana)
                bag.DropBanana(_countBranch, _pointToMove, this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bag))
        {
            if (_isBranch)
                PrefabMoveStock?.Invoke(_currentNumBranch);
            if (_isLog)
                PrefabMoveStock?.Invoke(_currentNumBranch);
            if (_isBarrel)
                PrefabMoveStock?.Invoke(_currentNumBranch);
            if (_isBanana)
                PrefabMoveStock?.Invoke(_currentNumBranch);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bug))
            bug.StopDropCoroutine();
    }

    private void ChangedCounter()
    {
        _numberBranch.text = _currentNumBranch.ToString() + " / " + _maxBranch.ToString();
    }

    public void RemoveNumber()
    {
        _countBranch--;
        Test?.Invoke(_countBranch);

        _currentNumBranch++;

        if (_isLogSpawner)
            _logSpawner.AddPrefab(1);

        if (_countBranch == 0)
            Opened?.Invoke();

        if (_numberBranch != null)
            ChangedCounter();
    }

    public void AddNumber()
    {
        _countBranch++;
        _currentNumBranch--;

        if (_numberBranch != null)
        {
            ChangedCounter();
        }
    }
}
