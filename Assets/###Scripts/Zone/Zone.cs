using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Zone : MonoBehaviour
{
    [SerializeField] private bool _isBranch;
    [SerializeField] private bool _isLog;

    [SerializeField] private Transform _pointToMove;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private int _countElement;

    //[SerializeField] private GameObject _tempPrefab;

    private int _currentNumber = 0;
    private int _maxElement;

    public UnityAction Opened;

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
            if (_isBranch)
                bag.DropBranch(_countElement, _pointToMove, this);

            if (_isLog)
            {
                bag.DropLog(_countElement, _pointToMove, this);
                Debug.Log("LOG!!!!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bug))
            bug.StopDropCoroutine();
    }

    private void ChangedCounter()
    {
        _number.text = (_maxElement - _currentNumber).ToString();
    }

    public void RemoveNumber()
    {
        _countElement--;

        _currentNumber++;

        if (_countElement == 0)
            // gameObject.SetActive(false);
            //_tempPrefab.gameObject.SetActive(true);
            Opened?.Invoke();

        if (_number != null)
            ChangedCounter();
    }
}
