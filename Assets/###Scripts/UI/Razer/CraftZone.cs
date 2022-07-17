using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftZone : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private LogSpawner _logSpawner;

    public UnityAction<bool> Enter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.ShakingTree();

            Enter?.Invoke(true);

            _progressBar.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.StopingShakeTree();

            Enter?.Invoke(false);

            _progressBar.Hide();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _progressBar.SetValueInstantly(_logSpawner._currentTime);

            //if (_logSpawner._numberPrefab == 0)
            //{
            //    _playerAnimator.StopingShakeTree();
            //}
        }
    }
}
