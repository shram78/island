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
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _imageEatWant;
    [SerializeField] private Zone _tempZone;
    [SerializeField] private AIObserver _aIObserver;
    [SerializeField] private Transform[] _points;

    private NavMeshAgent _navMeshAgent;

    private const string Speed = "Speed";

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
            bool isDrop = bag.DropBananaToMonkey(1, transform, _tempZone);
            if (isDrop)
                _aIObserver.SetCollectAI();
        }
    }

    private void FreePatrollMovement()
    {
        _navMeshAgent.SetDestination(_points[Random.Range(0, _points.Length)].position);
    }

    private void ChangeScale()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_imageEatWant.transform.DOScale(0f, 0f));
        sequence.Append(_imageEatWant.transform.DOScale(1f, 0.5f));

        sequence.Insert(3f, _imageEatWant.transform.DOScale(0f, 0.5f));
    }
}
