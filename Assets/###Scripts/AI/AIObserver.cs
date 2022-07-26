using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIObserver : MonoBehaviour
{
    [SerializeField] private GameObject _monkey;
    [SerializeField] private Zone _razerZone;
    [SerializeField] private float _timeReturnToPatrol = 60f;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _collecetedSpeed;


    private void OnEnable()
    {
        _razerZone.Opened += OnOpenedMonkey;
    }

    private void OnDisable()
    {
        _razerZone.Opened -= OnOpenedMonkey;
    }

    private void OnOpenedMonkey()
    {
        _monkey.gameObject.SetActive(true);

    }
    
    public void SetCollectAI()
    {
        _navMeshAgent.speed = _collecetedSpeed;

        _monkey.GetComponent<MonkeyAIPatrol>().enabled = false;
        _monkey.GetComponent<MonkeyAICollect>().enabled = true;

        StartCoroutine(SetPatrolTimer());
    }

    public void SetPatrolAI()
    {
        _navMeshAgent.speed = _patrolSpeed;

        _monkey.GetComponent<MonkeyAIPatrol>().enabled = true;
        _monkey.GetComponent<MonkeyAICollect>().enabled = false;
    }

    private IEnumerator SetPatrolTimer()
    {
        yield return new WaitForSeconds(_timeReturnToPatrol);

        SetPatrolAI();
    }
}
