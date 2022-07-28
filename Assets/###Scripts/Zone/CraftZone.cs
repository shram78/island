using DG.Tweening;
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
    private Coroutine _working;

    public UnityAction<bool> Enter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            if (_working == null)
                _working = StartCoroutine(CheckJoistickAndStartAnimations(playrsBag));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            if (_working != null)
            {
                StopCoroutine(_working);
                _working = null;
            }
        }
    }

    private IEnumerator CheckJoistickAndStartAnimations(PlayrsBag playrsBag)
    {
        Vector3 lookPoint = new Vector3(_logSpawner.transform.position.x, playrsBag.transform.position.y, _logSpawner.transform.position.z);

        while (true)
        {
            _progressBar.SetValueInstantly(_logSpawner._currentTime);

            if (_joystick.Direction == Vector2.zero && _logSpawner.IsHavePrefab)
            {
                if (!_isPlayerWorking)
                {
                    yield return new WaitForSeconds(0.2f);

                    StartWorking(lookPoint, playrsBag);
                }
            }
            else
            {
                if (_isPlayerWorking)
                    StoppedWorking();
            }

            yield return null;
        }
    }

    private void StartWorking(Vector3 lookPoint, PlayrsBag playrsBag)
    {
        _isPlayerWorking = true;

        _playerAnimator.Working(_isPlayerWorking);

        playrsBag.transform.DOLookAt(lookPoint, 0.5f, AxisConstraint.Y);

        Enter?.Invoke(_isPlayerWorking);

        _progressBar.Show();

    }

    private void StoppedWorking()
    {
        _isPlayerWorking = false;

        _playerAnimator.Working(_isPlayerWorking);

        Enter?.Invoke(_isPlayerWorking);

        _progressBar.Hide();
    }
}
