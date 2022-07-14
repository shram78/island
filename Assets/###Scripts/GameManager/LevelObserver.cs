using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObserver : MonoBehaviour
{
    [SerializeField] private Zone _firstPalm;
    [SerializeField] private GameObject _razerObserver;
    [SerializeField] private GameObject _secondPalm;
    [SerializeField] private GameObject _warerObserver;
    [SerializeField] private GameObject _joistick;

    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedZone;
    }

    private void Start()
    {
        StartCoroutine(SwitchJoistickTimer());
    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedZone;
    }

    private void OnOpenedZone()
    {
        _razerObserver.gameObject.SetActive(true);
        _secondPalm.gameObject.SetActive(true);
        _warerObserver.gameObject.SetActive(true);
    }

    private IEnumerator SwitchJoistickTimer()
    {
        yield return new WaitForSeconds(2f);
        _joistick.gameObject.SetActive(true);
    }
}
