using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]

public class MonkeyAICollect : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _emojyParticle;

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
        _isDancing = true;
        StartCoroutine(SetPatrolTimer());

        _emojyParticle.Play();

        _animator.SetTrigger(Dance);
    }

    private void Update()
    {
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
