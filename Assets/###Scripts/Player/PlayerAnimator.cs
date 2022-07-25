using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;

    private const string Speed = "Speed";
    private const string ShakeTree = "ShakeTree";
    private const string Dancing = "Dancing";


    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            if (Math.Abs(_joystick.Horizontal) > Math.Abs(_joystick.Vertical))
                PlayWalkAnimation(Math.Abs(_joystick.Horizontal));
            else
                PlayWalkAnimation(Math.Abs(_joystick.Vertical));
        }
        else
            PlayIdleAnimation();
    }

    private void PlayWalkAnimation(float value)
    {
        _animator.SetFloat(Speed, value);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetFloat(Speed, 0);
    }
    
    public void ShakingTree()
    {
        _animator.SetTrigger(ShakeTree);
    }

      public void Dance()
    {
        _animator.SetTrigger(Dancing);
    }
}
