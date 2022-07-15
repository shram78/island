using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObserverZone : MonoBehaviour
{
    [SerializeField] private Zone _zoneClosed;
    [SerializeField] private Zone _zoneOpened;
    [SerializeField] private float _heightToUp;
    [SerializeField] private float _timeToUp = 0.5f;

    private void OnEnable()
    {
        _zoneClosed.Opened += OnSetZone;
    }

    private void OnDisable()
    {
        _zoneClosed.Opened -= OnSetZone;
    }

    private void OnSetZone()
    {
        _zoneOpened.gameObject.SetActive(true);
        _zoneClosed.gameObject.SetActive(false);

        _zoneOpened.transform.DOLocalMoveY(_heightToUp, _timeToUp);

        _zoneOpened.transform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.4f);
    }
}
