using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _collider;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(3f);

        SetRigidbody();
    }

    private void SetRigidbody()
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }
}
