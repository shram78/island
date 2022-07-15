using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PalmGrowth : MonoBehaviour
{
    [SerializeField] private Zone _palmAlfa;

    private int _maxPalmCount;

    private void OnEnable()
    {
        _palmAlfa.Test += OnChangeHeight;
    }
    private void Start()
    {
        _maxPalmCount = _palmAlfa.CountPalm;
    }

    private void OnDisable()
    {
        _palmAlfa.Test -= OnChangeHeight;
    }

    private void OnChangeHeight(int countPalm) 
    {
        float currentPercent = (float)(_maxPalmCount - countPalm) / 10;

        transform.DOScale(currentPercent, 0.5f);
    }
}
