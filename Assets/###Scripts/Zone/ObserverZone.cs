using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverZone : MonoBehaviour
{
    [SerializeField] private Zone _zone;

    private void OnEnable()
    {
        _zone.Opened += OnSetZone;
    }

    private void OnDisable()
    {
        _zone.Opened -= OnSetZone;
    }

    private void OnSetZone()
    {
        Debug.LogError("snrbr zinme");
    }
}
