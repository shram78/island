using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeTreeZone : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PalmAnimator _palmAnimator;

    public UnityAction<bool> Enter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.ShakingTree();
            _palmAnimator.ShakingTree();

            Enter?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.StopingShakeTree();
            _palmAnimator.StopingShakeTree();

            Enter?.Invoke(false);
        }
    }

}
