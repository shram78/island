using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]


public class MonkeyAIPatrol : MonoBehaviour
{
    [SerializeField] private float _changePositionTime = 5f;
    [SerializeField] private float _moveDistance = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Image _imageEatWant;
    [SerializeField] private Zone _tempZone;
    [SerializeField] private AIObserver _aIObserver;

    private NavMeshAgent _navMeshAgent;

    private const string Speed = "Speed";

    private void Start()
    {
       // GetComponent<CapsuleCollider>().enabled = false;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(FreePatrollMovement), _changePositionTime, _changePositionTime);

        InvokeRepeating(nameof(ChangeScale), 0, 7);
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(FreePatrollMovement));
        CancelInvoke(nameof(ChangeScale));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bag))
        {
            bag.DropBarell(1, transform, _tempZone);

            _aIObserver.SetCollectAI();
        }
    }

    public Vector3 RandomNavSphere(float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);

        return navHit.position;
    }

    private void FreePatrollMovement()
    {
        _navMeshAgent.SetDestination(RandomNavSphere(_moveDistance));
    }

    private void ChangeScale()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_imageEatWant.transform.DOScale(0f, 0f));
        sequence.Append(_imageEatWant.transform.DOScale(1f, 0.5f));

        sequence.Insert(3f, _imageEatWant.transform.DOScale(0f, 0.5f));
    }
}
