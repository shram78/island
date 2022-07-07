using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeTreeZone : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PalmAnimator _palmAnimator;

    public UnityAction Enter;
    public UnityAction Exit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.ShakingTree();
            _palmAnimator.ShakingTree();

            Enter?.Invoke();//
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _playerAnimator.StopingShakeTree();
            _palmAnimator.StopingShakeTree();

            Exit?.Invoke();//
        }
    }

}
