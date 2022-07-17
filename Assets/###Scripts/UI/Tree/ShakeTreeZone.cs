using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeTreeZone : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PalmAnimator _palmAnimator;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Spawner _spawner;

    public UnityAction<bool> Enter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.ShakingTree();
            _palmAnimator.ShakingTree();

            Enter?.Invoke(true);

            _progressBar.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.StopingShakeTree();
            _palmAnimator.StopingShakeTree();

            Enter?.Invoke(false);

            _progressBar.Hide();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _progressBar.SetValueInstantly(_spawner._currentTime);

            //if (_spawner.CurrentCLosedPoint == 0)
            //{
            //    _playerAnimator.StopingShakeTree();
            //    _palmAnimator.StopingShakeTree();
            //}
            //else
            //{
            //    _playerAnimator.ShakingTree();
            //    _palmAnimator.ShakingTree();
            //}
        }
    }
}
