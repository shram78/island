using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevalObserver : MonoBehaviour
{
    [SerializeField] private Zone _firstPalm;
    [SerializeField] private GameObject _tentObserved;
    [SerializeField] private Zone _tent;
    [SerializeField] private GameObject _razerObserver;
    [SerializeField] private Zone _razer;
    [SerializeField] private GameObject _bridgeObserver;



    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedTent;
        _tent.Opened += OnOpenedRazer;
        _razer.Opened += OnOpenedBridge;

    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedTent;
        _tent.Opened -= OnOpenedRazer;
        _razer.Opened -= OnOpenedBridge;

    }

    private void OnOpenedTent()
    {
        _tentObserved.gameObject.SetActive(true);
    }

    private void OnOpenedRazer()
    {
        _razerObserver.gameObject.SetActive(true);
    }

     private void OnOpenedBridge()
    {
        _bridgeObserver.gameObject.SetActive(true);
    }
}

