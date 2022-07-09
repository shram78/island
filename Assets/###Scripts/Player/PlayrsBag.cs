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

    private int _currentStackPoint = 0;
    private List<CollectableItem> _collectableItems;
    private bool _isDrop;
    private int _currentStackSize = 5;

    private void Start()
    {
        _collectableItems = new List<CollectableItem>(); 
    }

    public void AddCoollectableItem(CollectableItem collectableItem)
    {
        // if (_currentStackPoint < _stackPoints.Length)
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
    }

    public void DropRedELement(int count, Transform pointMove, Zone zone)
    {
        if (count > 0)
        {
            _isDrop = true;
            StartCoroutine(StartRedDrop(count, pointMove, zone));
        }
    }

    public void StopDropCoroutine()
    {
        _isDrop = false;
    }

    private IEnumerator StartRedDrop(int count, Transform pointMove, Zone zone)
    {
        for (int j = 0; j < count; j++)
        {
            for (int i = _collectableItems.Count - 1; i >= 0; i--)
            {
                if (_isDrop)
                {
                    if (_collectableItems[i].IsBranch)
                    {
                        _collectableItems[i].transform.parent = null;

                        Sequence sequence = DOTween.Sequence();

                        _collectableItems[i].transform.DORotate(new Vector3(180, 0, 0), 0.5f).SetEase(Ease.Linear);
                        sequence.Append(_collectableItems[i].transform.DOMove(_movePathPointTwo.transform.position, 0.2f).SetEase(Ease.Linear));
                        sequence.Append(_collectableItems[i].transform.DOMove(_movePathPointOne.transform.position, 0.1f).SetEase(Ease.Linear));
                        sequence.Append(_collectableItems[i].transform.DOMove(pointMove.position, 0.2f).SetEase(Ease.Linear));

                        StartCoroutine(StartNumberRemove(zone, _collectableItems[i]));

                        RemoveElement(i);

                        StartCoroutine(DisplaceElement());

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator StartNumberRemove(Zone zone, CollectableItem collectableItem)
    {
        yield return new WaitForSeconds(0.5f);

        collectableItem.gameObject.SetActive(false);
        zone.RemoveNumber();
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
}
