using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObserver : MonoBehaviour
{
    [SerializeField] private Zone _firstPalm;
    [SerializeField] private Zone _tent;
    [SerializeField] private Zone _razer;
    [SerializeField] private Zone _water;
    [SerializeField] private GameObject _tentObserved;
    [SerializeField] private GameObject _razerObserver;
    [SerializeField] private GameObject _secondPalm;
    [SerializeField] private GameObject _waterObserver;
    [SerializeField] private GameObject _joistick;
    [SerializeField] private GameObject _raftObserver;

    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedTent;
        _tent.Opened += OnOpenedRazer;
        _razer.Opened += OnOpenedWater;
        _water.Opened += OnOpenedSecondPalm;
    }

    private void Start()
    {
        StartCoroutine(SwitchJoistickTimer());
    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedTent;
        _tent.Opened -= OnOpenedRazer;
        _razer.Opened -= OnOpenedWater;
        _water.Opened -= OnOpenedSecondPalm;
    }

    private void OnOpenedTent()
    {
        _tentObserved.gameObject.SetActive(true);
        _raftObserver.gameObject.SetActive(true);
    }

    private void OnOpenedRazer()
    {
        _razerObserver.gameObject.SetActive(true);
    }

    private void OnOpenedWater()
    {
        _waterObserver.gameObject.SetActive(true);
    }

    private void OnOpenedSecondPalm()
    {
        _secondPalm.gameObject.SetActive(true);
    }

    private IEnumerator SwitchJoistickTimer()
    {
        yield return new WaitForSeconds(2f);
        _joistick.gameObject.SetActive(true);
    }
}
