using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObserver : MonoBehaviour
{
    [SerializeField] private GameObject _monkey;
    [SerializeField] private Zone _razerZone;
    [SerializeField] private float _timeReturnToPatrol = 90f;

    public float TimeRemain => _timeReturnToPatrol;

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
        _monkey.GetComponent<MonkeyAIPatrol>().enabled = false;
        _monkey.GetComponent<MonkeyAICollect>().enabled = true;

        StartCoroutine(SetPatrolTimer());
    }

    public void SetPatrolAI()
    {
        _monkey.GetComponent<MonkeyAIPatrol>().enabled = true;
        _monkey.GetComponent<MonkeyAICollect>().enabled = false;
    }

    private IEnumerator SetPatrolTimer()
    {
        yield return new WaitForSeconds(_timeReturnToPatrol);

        SetPatrolAI();
    }
}
