using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]

public class MonkeyAICollect : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _emojyParticle;
    [SerializeField] private TMP_Text _textTimeRemain;
    [SerializeField] private AIObserver _aIObserver;

    private float _currentTimeReamin;
    private int _destanationPoint = 0;
    private NavMeshAgent _agent;
    private bool _isDancing = false;

    private const string Speed = "Speed";
    private const string Dance = "Dance";


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _currentTimeReamin = _aIObserver.TimeRemain;

        _textTimeRemain.gameObject.SetActive(true);

        _isDancing = true;
        StartCoroutine(SetPatrolTimer());

        _emojyParticle.Play();

        _animator.SetTrigger(Dance);
    }

    private void OnDisable()
    {
        _textTimeRemain.gameObject.SetActive(false);
    }

    private void Update()
    {
        _currentTimeReamin -= Time.deltaTime;

        int showPoint = Convert.ToInt32(_currentTimeReamin);

        _textTimeRemain.text = "..." + showPoint.ToString();

        _animator.SetFloat(Speed, _agent.velocity.magnitude);

        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if (_isDancing)
            _agent.isStopped = true;
        else
            _agent.isStopped = false;
    }

    private void GotoNextPoint()
    {
        if (_points.Length == 0)
            return;

        _agent.destination = _points[_destanationPoint].position;

        _destanationPoint = (_destanationPoint + 1) % _points.Length;
    }

    private IEnumerator SetPatrolTimer()
    {
        yield return new WaitForSeconds(5);
        _isDancing = false;
    }
}
