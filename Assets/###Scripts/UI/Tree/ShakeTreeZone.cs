using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ShakeTreeZone : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PalmAnimator _palmAnimator;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private PlayerWork _playerWork;

    private bool _isPlayerWorking = false;


    public UnityAction<bool> Enter;

    private void Update()
    {
        if (_isPlayerWorking)
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
                StoppedWorking();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        else if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            StartCoroutine(CheckJoistickAndStartAnimations(other));
    }

    private IEnumerator CheckJoistickAndStartAnimations(Collider other)
    {
        yield return new WaitForSeconds(0.5f);

        if (_joystick.Horizontal == 0 || _joystick.Vertical == 0)
        {
            if (other.gameObject.TryGetComponent(out PlayrsBag bag))
            {
                _isPlayerWorking = true;

                _playerWork.ShowLog();

                _playerAnimator.ShakingTree();
                _palmAnimator.ShakingTree();

                Enter?.Invoke(true);

                _progressBar.Show();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        else if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            StoppedWorking();
    }


    private void StoppedWorking()
    {
        _isPlayerWorking = false;

        _playerWork.HideLog();

        _palmAnimator.StopingShakeTree();

        Enter?.Invoke(false);

        _progressBar.Hide();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        else if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            _progressBar.SetValueInstantly(_spawner._currentTime);
    }
}
