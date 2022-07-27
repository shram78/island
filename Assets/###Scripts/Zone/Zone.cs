using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;

public class Zone : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private bool _isTrashCan;

    [SerializeField] private Transform _pointToMove;
    [SerializeField] private TMP_Text _numberBranch;
    [SerializeField] private int _countBranch;

    [SerializeField] private bool _isLogSpawner;
    [SerializeField] private LogSpawner _logSpawner;
    [SerializeField] private FloatingJoystick _joystick;

    private int _currentNumBranch = 0;
    private int _maxBranch;
    private Coroutine _stacking;

    public UnityAction Opened;
    public UnityAction<int> BarrelsDropToPalm;
    public UnityAction<int> DropPrefabInStock;


    public int CountPalm => _countBranch;

    private void Start()
    {
        if (_numberBranch != null)
        {
            _maxBranch = _countBranch;
            ChangedCounter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bag))
        {
            if (_isTrashCan)
            {
                bag.TrashBag(_pointToMove, this);
                return;
            }

            if (_stacking == null)
                _stacking = StartCoroutine(Stacking(bag));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bug))
        {
            if (_stacking != null)
            {
                StopCoroutine(_stacking);
                _stacking = null;
            }

            bug.StopDropItem(this);
        }
    }

    private IEnumerator Stacking(PlayrsBag bag)
    {

        while (_currentNumBranch < _maxBranch)
        {
            yield return new WaitForSeconds(0.5f);

            if (_joystick.Direction != Vector2.zero)
            {
                yield return null;
                continue;
            }

            if (bag.IsDropping)
            {
                yield return null;
                continue;
            }

            if (!bag.IsContainItem(_type))
            {
                yield return null;
                continue;
            }

            bag.DropItem(_countBranch, _pointToMove, this, _type);

            yield return null;
        }
    }

    public void RemoveNumber()
    {
        _countBranch--;
        BarrelsDropToPalm?.Invoke(_countBranch);

        _currentNumBranch++;

        if (_currentNumBranch == _maxBranch & _stacking != null)
        {
            StopCoroutine(_stacking);
            _stacking = null;
        }

        if (_isLogSpawner)
            _logSpawner.AddPrefab(1);

        if (_countBranch == 0)
            Opened?.Invoke();

        if (_numberBranch != null)
            ChangedCounter();

        DropPrefabInStock?.Invoke(_currentNumBranch);
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

    private void ChangedCounter()
    {
        _numberBranch.text = _currentNumBranch.ToString() + " / " + _maxBranch.ToString();
    }
}
