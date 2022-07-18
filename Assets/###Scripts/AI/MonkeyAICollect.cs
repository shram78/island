using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MonkeyAICollect : MonoBehaviour
{
    //[SerializeField] private Animator _animator;
    //[SerializeField] private List<Transform> _targets;

    //private NavMeshAgent _navMeshAgent;
    //private int _currentPoint;

    //private const string Speed = "Speed";

    //private void Start()
    //{
    //    _navMeshAgent = GetComponent<NavMeshAgent>();
    //}

    //private void Update()
    //{
    //    _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);

    //    if (_navMeshAgent.transform.position == _navMeshAgent.pathEndPosition)
    //    {
    //        TargetsUpdate();
    //    }
    //}

    //private void TargetsUpdate()
    //{
    //    _currentPoint++;
    //    if (_currentPoint >= _targets.Count)
    //        _currentPoint = 0;

    //    _navMeshAgent.SetDestination(_targets[_currentPoint].position);
    //   // StartCoroutine(SetNewPointTimer());
    //}

    //private IEnumerator SetNewPointTimer()
    //{
    //    yield return new WaitForSeconds(1f);

    //    _navMeshAgent.SetDestination(_targets[_currentPoint].position);
    //}

    [SerializeField] private Transform[] _points;
    [SerializeField] private Animator _animator;

    private int _destanationPoint = 0;
    private NavMeshAgent _agent;

    private const string Speed = "Speed";

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _agent.velocity.magnitude);

        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
             GotoNextPoint();
    }

    private void GotoNextPoint()
    {
        if (_points.Length == 0)
            return;

        _agent.destination = _points[_destanationPoint].position;

        _destanationPoint = (_destanationPoint + 1) % _points.Length;
    }
}
