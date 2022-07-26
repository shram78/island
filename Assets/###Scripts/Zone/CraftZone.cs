using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CraftZone : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private LogSpawner _logSpawner;
    [SerializeField] private FloatingJoystick _joystick;

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

                _playerAnimator.Working();

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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        else if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            _progressBar.SetValueInstantly(_logSpawner._currentTime);
    }

    private void StoppedWorking()
    {
        _isPlayerWorking = false;

        Enter?.Invoke(false);

        _progressBar.Hide();
    }
}
