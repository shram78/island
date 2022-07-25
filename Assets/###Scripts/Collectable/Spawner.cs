using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CollectableItem _collectableItem;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private ShakeTreeZone _shakeTreeZone;
    [SerializeField] private float _currentDdelaySpawn = 10;
    [SerializeField] private float _delayManualSpawn = 2;
    [SerializeField] private ParticleSystem _spawnPaticle;

    private float _delayAutoSpawn;


    private int _numberPrefab = 0;
    private CollectableItem _currentPrefab;
    private int _numberClosedPoints = 0;

    public float _currentTime { get; private set; }

    public int CurrentCLosedPoint { get; private set; }

    private void OnEnable()
    {
        CurrentCLosedPoint = _spawnPoints.Length - _numberClosedPoints;

        _shakeTreeZone.Enter += OnSetNewSppeed;
    }

    private void Start()
    {
        _delayAutoSpawn = _currentDdelaySpawn;
    }

    private void Update()
    {
        //  if (_numberPrefab > 0)
        {
            if (_numberClosedPoints < _spawnPoints.Length)
            {
                _currentTime += Time.deltaTime;
            }

            if (_currentTime >= _delayAutoSpawn)
            {
                _currentTime = 0;

                SpawnPrefab();
            }
        }
        CurrentCLosedPoint = _spawnPoints.Length - _numberClosedPoints;
    }

    private void OnDisable()
    {
        _shakeTreeZone.Enter -= OnSetNewSppeed;
    }

    private void SpawnPrefab()
    {
        if (_numberClosedPoints < _spawnPoints.Length)
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                if (_spawnPoints[i].IsClose == false)
                {
                    _currentPrefab = Instantiate(_collectableItem, _spawnPoints[i].transform.position, Quaternion.identity);

                    _spawnPoints[i].ChangeFreeStatus(true);

                    _currentPrefab.InitSpawnPoint(_spawnPoints[i]);

                    _currentPrefab.Selected += OnSelected;
                    _numberClosedPoints++;

                    AddPrefab(-1);

                    _spawnPaticle.Play();

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

    public void OnSetNewSppeed(bool isEnter)
    {
        if (isEnter)
            _delayAutoSpawn = _delayManualSpawn;
        if (isEnter == false)
            _delayAutoSpawn = _currentDdelaySpawn;
    }
}
