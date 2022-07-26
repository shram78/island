using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PalmAnimator : MonoBehaviour
{
    private const string IsPlayerWork = "IsPlayerWork";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShakingTree()
    {
        _animator.SetBool(IsPlayerWork, true);
    }

    public void StopingShakeTree()
    {
        _animator.SetBool(IsPlayerWork, false);
    }
}
