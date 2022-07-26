using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Log : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(Random.Range(4f, 7f));

        SetRigidbody();
    }

    private void SetRigidbody()
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }
}
