using System.Collections;
using UnityEngine;

public class SecondLevalObserver : MonoBehaviour
{
    [SerializeField] private Zone _firstPalm;
    [SerializeField] private GameObject _tentObserved;
    [SerializeField] private Zone _tent;
    [SerializeField] private GameObject _razerObserver;
    [SerializeField] private Zone _razer;
    [SerializeField] private GameObject _bridgeObserver;
    [SerializeField] private GameObject _boat;
    [SerializeField] private Zone _bridgeZone;
    [SerializeField] private GameObject _waterObserver;
    [SerializeField] private GameObject _bananaObserver;
    [SerializeField] private GameObject _secondRazerObserver;
    [SerializeField] private GameObject _secondPalm;
    [SerializeField] private GameObject _joistick;


    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedTent;
        _tent.Opened += OnOpenedRazer;
        _razer.Opened += OnOpenedBridge;
        _bridgeZone.Opened += OnOpenedWater;

    }
     private void Start()
    {
        StartCoroutine(SwitchJoistickTimer());
    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedTent;
        _tent.Opened -= OnOpenedRazer;
        _razer.Opened -= OnOpenedBridge;
        _bridgeZone.Opened -= OnOpenedWater;


    }

    private void OnOpenedTent()
    {
        _tentObserved.gameObject.SetActive(true);
        _boat.gameObject.SetActive(true);
        _bananaObserver.gameObject.SetActive(true);
    }

    private void OnOpenedRazer()
    {
        _razerObserver.gameObject.SetActive(true);
    }

    private void OnOpenedBridge()
    {
        _bridgeObserver.gameObject.SetActive(true);
    }

    private void OnOpenedWater()
    {
        _waterObserver.gameObject.SetActive(true);
        _secondRazerObserver.gameObject.SetActive(true);
        _secondPalm.gameObject.SetActive(true);
    }

       private IEnumerator SwitchJoistickTimer()
    {
        yield return new WaitForSeconds(2f);
        _joistick.gameObject.SetActive(true);
    }
}

