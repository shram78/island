using UnityEngine;
using UnityEngine.AI;

public class MonkeyAI : MonoBehaviour
{
    [SerializeField] private float _changePositionTime = 5f;
    [SerializeField] private float _moveDistance = 10f;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;

    private NavMeshAgent _navMeshAgent;

    private const string Speed = "Speed";

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;

        InvokeRepeating(nameof(Move), _changePositionTime, _changePositionTime);
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude / _moveSpeed / 2);
    }

    public Vector3 RandomNavSphere(float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);

        return navHit.position;
    }

    private void Move()
    {
        _navMeshAgent.SetDestination(RandomNavSphere(_moveDistance));
    }
}
