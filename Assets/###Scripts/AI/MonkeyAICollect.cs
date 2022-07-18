using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]

public class MonkeyAICollect : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Animator _animator;

    private int _destanationPoint = 0;
    private NavMeshAgent _agent;

    private const string Speed = "Speed";

    private void Start()
    {
       // GetComponent<CapsuleCollider>().enabled = true;

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
