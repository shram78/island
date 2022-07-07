using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CollectableItem _collectableItem;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _delaySpawn;

    private int _numberPrefab = 0;
    private CollectableItem _currentPrefab;
    private float _currentTime;
    private int _numberClosedPoints = 0;

    private void Update()
    {
      //  if (_numberPrefab > 0)
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
                    
                    _currentPrefab.MoveDown();

                    _spawnPoints[i].ChangeFreeStatus(true);
                    
                    _currentPrefab.InitSpawnPoint(_spawnPoints[i]);

                    _currentPrefab.Selected += OnSelected;
                    _numberClosedPoints++;

                    AddPrefab(-1);

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
}
