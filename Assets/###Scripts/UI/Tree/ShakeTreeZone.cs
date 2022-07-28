using System.Collections;
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

    private IEnumerator CheckJoistickAndStartAnimations(PlayrsBag bag)
    {

        Vector3 lookPoint = new Vector3(_spawner.transform.position.x, _playerWork.transform.position.y, _spawner.transform.position.z);

        while (true)
        {
            _progressBar.SetValueInstantly(_spawner._currentTime);

            if (_joystick.Direction == Vector2.zero)
            {
                if (!_isPlayerWorking)
                {
                    yield return new WaitForSeconds(0.2f);

                    StartWorking(lookPoint);
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

    private void StartWorking(Vector3 lookPoint)
    {
        _isPlayerWorking = true;

        _playerWork.ShowLog();

        _playerAnimator.ShakingTree(_isPlayerWorking);
        _palmAnimator.ShakingTree();

        Enter?.Invoke(true);

        _progressBar.Show();

        _playerWork.transform.DOLookAt(lookPoint, 0.5f, AxisConstraint.Y);
    }

    private void StoppedWorking()
    {
        _isPlayerWorking = false;

        _playerWork.HideLog();

        _playerAnimator.ShakingTree(_isPlayerWorking);
        _palmAnimator.StopingShakeTree();

        Enter?.Invoke(false);

        _progressBar.Hide();
    }
}
