using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private ParticleSystem _logParticle;

    private const string Speed = "Speed";
    private const string ShakeTree = "ShakeTree";
    private const string Work = "Work";
    private const string Dancing = "Dancing";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_joystick.Direction != Vector2.zero)
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

    public void ShakingTree(bool isShaking)
    {
        _animator.SetBool(ShakeTree, isShaking);
    }

    public void Working(bool isWorking)
    {
        _animator.SetBool(Work, isWorking);
    }

    public void Dance()
    {
        _animator.SetTrigger(Dancing);
    }

    public void PlayParticleLog()
    {
        _logParticle.Play();
    }
}
