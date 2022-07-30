using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayrsBag : MonoBehaviour
{
    [SerializeField] private GameObject[] _stackPoints;
    [SerializeField] private GameObject _bag;
    [SerializeField] private GameObject _movePathPointOne;
    [SerializeField] private GameObject _movePathPointTwo;
    [SerializeField] private GameObject _textFull;
    [SerializeField] private int _currentStackSize = 5;

    private int _currentStackPoint = 0;
    private List<CollectableItem> _collectableItems;
    private bool _isDropping = false;

    private Coroutine _droppingItem;

    public bool IsDropping => _isDropping;

    private void Start()
    {
        _collectableItems = new List<CollectableItem>();
    }

    public void AddCoollectableItem(CollectableItem collectableItem)
    {
        if (_currentStackPoint < _currentStackSize)
        {
            collectableItem.transform.SetParent(_bag.transform);

            Sequence sequence = DOTween.Sequence();
            collectableItem.transform.DOLocalRotate(new Vector3(180, 180, 180), 0.5f).SetEase(Ease.Linear);
            sequence.Append(collectableItem.transform.DOLocalMove(_movePathPointOne.transform.localPosition, 0.2f).SetEase(Ease.Linear));
            sequence.Append(collectableItem.transform.DOLocalMove(_movePathPointTwo.transform.localPosition, 0.1f).SetEase(Ease.Linear));
            sequence.Append(collectableItem.transform.DOLocalMove(_stackPoints[_currentStackPoint].transform.localPosition, 0.2f).SetEase(Ease.Linear));

            _collectableItems.Add(collectableItem);
            _currentStackPoint++;

            collectableItem.Select();
        }

        if (_currentStackPoint >= _currentStackSize)
        {
            _textFull.gameObject.SetActive(true);
            StartCoroutine(SwitchOffTimer());
        }
    }

    private IEnumerator SwitchOffTimer()
    {
        yield return new WaitForSeconds(2f);

        _textFull.gameObject.SetActive(false);

    }

    public bool IsContainItem(ItemType type)
    {
        foreach (var item in _collectableItems)
        {
            if (item.Type == type)
                return true;
        }

        return false;
    }

    public void DropItem(int count, Transform pointMove, Zone zone, ItemType type)
    {

        if (count > 0)
        {
            if (_isDropping == false)
            {
                _isDropping = true;
                _droppingItem = StartCoroutine(DroppingItem(count, pointMove, zone, type));
            }
        }
    }

    public void StopDropItem(Zone zone)
    {
        if (_isDropping == true)
        {
            if (_droppingItem != null)
            {
                StopCoroutine(_droppingItem);
                _droppingItem = null;
            }

            _isDropping = false;
        }
    }

    public void TrashBag(Transform pointMove, Zone zone)
    {
        StartCoroutine(TrashingItem(pointMove, zone));
    }

    private IEnumerator DroppingItem(int count, Transform pointMove, Zone zone, ItemType type)
    {
        for (int j = 0; j < count; j++)
        {
            for (int i = _collectableItems.Count - 1; i >= 0; i--)
            {
                if (_isDropping)
                {
                    if (_collectableItems[i].Type == type)
                    {
                        RemoveItem(_collectableItems[i], pointMove, zone, i);

                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.2f);
        }

        _isDropping = false;
        _droppingItem = null;
    }

    private IEnumerator TrashingItem(Transform pointMove, Zone zone)
    {
        for (int i = _collectableItems.Count - 1; i >= 0; i--)
        {
            RemoveItem(_collectableItems[i], pointMove, zone, i);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void RemoveItem(CollectableItem item, Transform pointMove, Zone zone, int index)
    {
        item.transform.parent = null;

        Sequence sequence = DOTween.Sequence();

        item.transform.DORotate(new Vector3(180, 0, 0), 0.5f).SetEase(Ease.Linear);
        sequence.Append(item.transform.DOMove(_movePathPointTwo.transform.position, 0.2f).SetEase(Ease.Linear));
        sequence.Append(item.transform.DOMove(_movePathPointOne.transform.position, 0.1f).SetEase(Ease.Linear));
        sequence.Append(item.transform.DOMove(pointMove.position, 0.2f).SetEase(Ease.Linear));

        StartCoroutine(StartNumberRemove(zone, item));

        RemoveElement(index);

        StartCoroutine(DisplaceElement());
    }

    private IEnumerator StartNumberRemove(Zone zone, CollectableItem collectableItem)
    {
        zone.RemoveNumber();

        yield return new WaitForSeconds(0.5f);

        collectableItem.gameObject.SetActive(false);
    }

    private void RemoveElement(int index)
    {
        _collectableItems.RemoveAt(index);
        _currentStackPoint--;
    }

    private IEnumerator DisplaceElement()
    {
        yield return new WaitForSeconds(0f);

        for (int i = 0; i < _collectableItems.Count; i++)
        {
            _collectableItems[i].transform.position = _stackPoints[i].transform.position;
        }
    }

    public bool DropBananaToMonkey(int count, Transform pointMove, Zone zone)
    {
        foreach (var item in _collectableItems)
        {
            if (item.Type == ItemType.Banana)
            {
                _isDropping = true;
                StartCoroutine(DroppingItem(count, pointMove, zone, ItemType.Banana));
                return true;
            }
        }
        return false;
    }

    public void StackUp()
    {
        _currentStackSize += 5;

        if (_currentStackSize > _stackPoints.Length)
            _currentStackSize = _stackPoints.Length;
    }
}