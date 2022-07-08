using UnityEngine;
using TMPro;

public class Zone : MonoBehaviour
{
    [SerializeField] private bool _isRed;
    [SerializeField] private Transform _pointToMove;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private int _countElement;

    [SerializeField] private GameObject _tempPrefab;

    private int _currentNumber = 0;
    private int _maxElement;

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
            if (_isRed)
                bag.DropRedELement(_countElement, _pointToMove, this);
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
            _tempPrefab.gameObject.SetActive(true);

        if (_number != null)
            ChangedCounter();
    }
}
