using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private bool _isRed;
    [SerializeField] private bool _isFiol;
    [SerializeField] private bool _isGreen;
    [SerializeField] private bool _isBlue;

    private bool _isSelect = false;
    private SpawnPoint _spawnPoint;

    public bool IsRed => _isRed;
    public bool IsFiol => _isFiol;
    public bool IsGreen => _isGreen;
    public bool IsBlue => _isBlue;

    public event UnityAction<CollectableItem> Selected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            Debug.LogError(other.name);
            if (_isSelect == false)
            {
                playrsBag.AddCoollectableItem(this);

                Selected?.Invoke(this);
            }
        }
    }

    public void Select()
    {
        _isSelect = true;
    }

    public void SaveSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    //public SpawnPoint ReturnPoint()
    //{
    //    return _spawnPoint;
    //}
}
