using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] private CollectableItem _prefabSpawm;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Transform[] _deliveryPoint;
    [SerializeField] private Zone _zone;
    [SerializeField] private CraftZone _craftZone;
    [SerializeField] private GameObject[] _branchInStocks;
    [SerializeField] private Vector3 _jumpVectorPrefab;
    [SerializeField] private float _timeToMovePrefab = 3;

    public int _numberPrefab = 0;
    private CollectableItem _currentPrefab;
    private int _numberClosedPoints = 0;
    private bool _isInZone = false;
    private float _delaySpawn = 2f;

    public float _currentTime { get; private set; }

    public int CurrentCLosedPoint { get; private set; }

    private void OnEnable()
    {
        CurrentCLosedPoint = _spawnPoints.Length - _numberClosedPoints;

        _craftZone.Enter += SetInzoneStatus;
        _zone.PrefabMoveStock += ShowBranchnStock;
    }
    private void OnDisable()
    {
        _craftZone.Enter -= SetInzoneStatus;
        _zone.PrefabMoveStock -= ShowBranchnStock;
    }

    private void SetInzoneStatus(bool isInZone)
    {
        _isInZone = isInZone;
    }

    private void Update()
    {
        if (_numberPrefab > 0 && _isInZone)
        {
            if (_numberClosedPoints < _spawnPoints.Length)
            {
                _currentTime += Time.deltaTime;
            }

            if (_currentTime >= _delaySpawn)
            {
                _currentTime = 0;

                SpawnPrefab();
            }
        }
        CurrentCLosedPoint = _spawnPoints.Length - _numberClosedPoints;
    }

    private void SpawnPrefab()
    {
        if (_numberClosedPoints < _spawnPoints.Length)
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                if (_spawnPoints[i].IsClose == false)
                {
                    _currentPrefab = Instantiate(_prefabSpawm, _spawnPoints[i].transform.position, Quaternion.identity);

                    _spawnPoints[i].ChangeFreeStatus(true);

                    _currentPrefab.InitSpawnPoint(_spawnPoints[i]);

                    HideBranchnStock(_numberClosedPoints);

                    _currentPrefab.Selected += OnSelected;
                    _numberClosedPoints++;

                    AddPrefab(-1);

                    _zone.AddNumber();
                    MoveToDelivery(_currentPrefab);

                    break;
                }
            }
        }
    }

    public void AddPrefab(int count)
    {
        _numberPrefab += count;
    }

    private void OnSelected(CollectableItem collectableItem)
    {
        collectableItem.ReturnPoint().ChangeFreeStatus(false);
        _numberClosedPoints--;

        collectableItem.Selected -= OnSelected;
    }

    private void MoveToDelivery(CollectableItem currentLog)
    {
        currentLog.transform.DOLocalMove(_deliveryPoint[Random.Range(0, _deliveryPoint.Length)].position, _timeToMovePrefab);
    }

    private void ShowBranchnStock(int currentNumber)
    {
        for (int i = 0; i < currentNumber; i++)
            _branchInStocks[i].gameObject.SetActive(true);
    }

    private void HideBranchnStock(int currentNumber)
    {
        GameObject log = _branchInStocks.FirstOrDefault(p => p.activeSelf == true);

        Vector3 startPosition = log.transform.position;
        log.transform.DOLocalJump(_jumpVectorPrefab, 1, 1, 1);
        StartCoroutine(StartTimer(log, startPosition));
    }

    private IEnumerator StartTimer(GameObject log, Vector3 startPosition)
    {
        yield return new WaitForSeconds(1f);
        log.transform.position = startPosition;
        log.gameObject.SetActive(false);
    }
}
