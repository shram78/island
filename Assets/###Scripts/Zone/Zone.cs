using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;

public class Zone : MonoBehaviour
{
    [SerializeField] private bool _isBranch;
    [SerializeField] private bool _isLog;
    [SerializeField] private bool _isBarrel;

    [SerializeField] private Transform _pointToMove;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private int _countElement;

    [SerializeField] private bool _isLogSpawner;
    [SerializeField] private LogSpawner _logSpawner;

    private int _currentNumBranch = 0;
    private int _maxElement;

    public UnityAction Opened;
    public UnityAction<int> PrefabMoveStock;


    private void Start()
    {
        if (_number != null)
        {
            _maxElement = _countElement;
            ChangedCounter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bag))
        {
           // StartCoroutine(StartTimer(bag));
            {
                if (_isBranch)
                    bag.DropBranch(_countElement, _pointToMove, this);

                if (_isLog)
                    bag.DropLog(_countElement, _pointToMove, this);

                if (_isBarrel)
                    bag.DropBarell(_countElement, _pointToMove, this);
            }
        }
    }

    private IEnumerator StartTimer(PlayrsBag bag)
    {
        yield return new WaitForSeconds(0.5f);

        if (_isBranch)
            bag.DropBranch(_countElement, _pointToMove, this);

        if (_isLog)
            bag.DropLog(_countElement, _pointToMove, this);

        if (_isBarrel)
            bag.DropBarell(_countElement, _pointToMove, this);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bug))
            bug.StopDropCoroutine();
    }

    private void ChangedCounter()
    {
        _number.text = _currentNumBranch.ToString() + " / " + _maxElement.ToString();
    }

    public void RemoveNumber()
    {
        _countElement--;

        _currentNumBranch++;

        if (_isLogSpawner)
            _logSpawner.AddPrefab(1);

        if (_countElement == 0)
            Opened?.Invoke();

        if (_number != null)
            ChangedCounter();
    }

    public void AddNumber()
    {
        _countElement++;
        _currentNumBranch--;

        if (_number != null)
        {
            ChangedCounter();
        }
    }
}
