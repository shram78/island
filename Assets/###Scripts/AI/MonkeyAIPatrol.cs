using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MonkeyAIPatrol : MonoBehaviour
{
    [SerializeField] private float _changePositionTime = 5f;
    [SerializeField] private float _moveDistance = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;

    private NavMeshAgent _navMeshAgent;
    private const string Speed = "Speed";

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;

         InvokeRepeating(nameof(FreePatrollMovement), _changePositionTime, _changePositionTime);
    }
    private void Update()
    {
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
    }

    private void OnDisable()
    {
        CancelInvoke("FreePatrollMovement");
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
}
