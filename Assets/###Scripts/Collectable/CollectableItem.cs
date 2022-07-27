using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class CollectableItem : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private float _timeOfPhysicalMove;
    [SerializeField] private BoxCollider _triggerCollider;
    [SerializeField] private CapsuleCollider _physicalCollider;

    private bool _isSelect = false;
    private SpawnPoint _spawnPoint;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public ItemType Type => _type;

    public UnityAction<CollectableItem> Selected;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _coroutine = StartCoroutine(CountingDownPhysicalTime());

        _triggerCollider.isTrigger = true;
        _physicalCollider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            if (_isSelect == false)
            {
                if (_rigidbody.isKinematic == false)
                {
                    StopCoroutine(_coroutine);
                    SetRigidbody();
                }

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

    private IEnumerator CountingDownPhysicalTime()
    {
        yield return new WaitForSeconds(_timeOfPhysicalMove);

        SetRigidbody();
    }

    private void SetRigidbody()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _physicalCollider.enabled = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

}

public enum ItemType
{
    Log,
    Branch,
    Barell,
    Banana
}
