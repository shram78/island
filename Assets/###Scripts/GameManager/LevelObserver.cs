using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObserver : MonoBehaviour
{
    [SerializeField] private Zone _firstPalm;
    [SerializeField] private GameObject _razerObserver;
    [SerializeField] private GameObject _secondPalm;

    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedZone;
    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedZone;
    }

    private void OnOpenedZone()
    {
        _razerObserver.gameObject.SetActive(true);
        _secondPalm.gameObject.SetActive(true);
    }
}
