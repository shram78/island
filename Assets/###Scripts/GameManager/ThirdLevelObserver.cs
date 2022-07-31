using UnityEngine;

public class ThirdLevelObserver : MonoBehaviour
{
    [SerializeField] private Zone _firstPalm;
    [SerializeField] private GameObject _tentObserved;
    [SerializeField] private GameObject _lightHouseObserver;
    [SerializeField] private GameObject _bananaObserver;
    [SerializeField] private Zone _tent;
    [SerializeField] private GameObject _razerObserver;
    [SerializeField] private GameObject _waterObserver;
    [SerializeField] private Zone _lightHouse;
    [SerializeField] private GameObject _shipObserver;
    [SerializeField] private GameObject _secondPalmObserver;
    [SerializeField] private GameObject _thirdPalmObserver;
    [SerializeField] private GameObject _fourthPalmObserver;
    [SerializeField] private GameObject _fifthPalmObserver;
    [SerializeField] private GameObject _secondRazer;
    [SerializeField] private GameObject _secondBananaObserver;



    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedTent;
        _tent.Opened += OnOpenedRazer;
        _lightHouse.Opened += OnShipSpawn;
    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedTent;
        _tent.Opened -= OnOpenedRazer;
        _lightHouse.Opened -= OnShipSpawn;
    }

    private void OnOpenedTent()
    {
        _tentObserved.gameObject.SetActive(true);
        _lightHouseObserver.gameObject.SetActive(true);
        _bananaObserver.gameObject.SetActive(true);
        _secondPalmObserver.gameObject.SetActive(true);
    }

    private void OnOpenedRazer()
    {
        _razerObserver.gameObject.SetActive(true);
        _waterObserver.gameObject.SetActive(true);
        _thirdPalmObserver.gameObject.SetActive(true);
    }

    private void OnShipSpawn()
    {
        _shipObserver.gameObject.SetActive(true);
        _fourthPalmObserver.gameObject.SetActive(true);
        _fifthPalmObserver.gameObject.SetActive(true);
        _secondRazer.gameObject.SetActive(true);
        _secondBananaObserver.gameObject.SetActive(true);
    }
}
