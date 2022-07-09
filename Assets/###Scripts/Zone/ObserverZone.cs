using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObserverZone : MonoBehaviour
{
    [SerializeField] private Zone _zoneClosed;
    [SerializeField] private Zone _zoneOpened;
    [SerializeField] private float _heightToUp;

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

        _zoneOpened.transform.DOLocalMoveY(_heightToUp, 0.5f);
    }
}
