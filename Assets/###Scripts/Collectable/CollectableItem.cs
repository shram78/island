using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private bool _isBranch;
    [SerializeField] private bool _isLog;
    [SerializeField] private bool _isBarrel;
    [SerializeField] private bool _isBlue;

    private bool _isSelect = false;
    private SpawnPoint _spawnPoint;

    public bool IsBranch => _isBranch;
    public bool IsLog => _isLog;
    public bool IsBarrel => _isBarrel;
    public bool IsBlue => _isBlue;

    public UnityAction<CollectableItem> Selected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
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

    public void InitSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    public SpawnPoint ReturnPoint()
    {
        return _spawnPoint;
    }
}
