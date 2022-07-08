using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]


public class Branch : MonoBehaviour
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
        yield return new WaitForSeconds(2f);

        SetRigidbody();
    }

    private void SetRigidbody()
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }
}
