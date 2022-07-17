using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PalmAnimator : MonoBehaviour
{
    private const string ShakeTree = "ShakeTree";
    private const string StopShakeTree = "StopShakeTree";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShakingTree()
    {
         _animator.SetTrigger(ShakeTree);
    }

    public void StopingShakeTree()
    {
        _animator.SetTrigger(StopShakeTree);
    }
}
