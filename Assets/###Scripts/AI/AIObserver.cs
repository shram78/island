using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObserver : MonoBehaviour
{
    [SerializeField] private GameObject _monkey;
    [SerializeField] private Zone _firstPalm;

    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedMonkey;
    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedMonkey;
    }

    private void OnOpenedMonkey()
    {
        _monkey.gameObject.SetActive(true);

        StartCoroutine(XXXTimer());
    }

    private IEnumerator XXXTimer()
    {
        yield return new WaitForSeconds(20f);

        _monkey.GetComponent<MonkeyAIPatrol>().enabled = false;
        _monkey.GetComponent<MonkeyAICollect>().enabled = true;
    }
}
