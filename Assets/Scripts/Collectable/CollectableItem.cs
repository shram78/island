using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

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

    public void MoveDown()
    {
        transform.DOLocalMoveY(-3.5f, 0.5f).SetEase(Ease.InBack).SetRelative();
    }
}
