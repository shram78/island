using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class LevelComlete : MonoBehaviour
{
    [SerializeField] ParticleSystem _confettyParticle;
    [SerializeField] private Transform _arivvedPoint;
    [SerializeField] private GameObject _raftRoot;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _levelCompleteCamera;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private GameObject _joistickHandler;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private GameObject _sail;
    [SerializeField] private GameObject _UIFood;

    private bool _isFoofCollected = false;

    public void SetFoofCollected()
    {
        _isFoofCollected = true;
        _UIFood.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isFoofCollected)
        {
            if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            {
                ArriveFromIsland(playrsBag);
            }
        }
    }

    private void ArriveFromIsland(PlayrsBag playrsBag)
    {
        DisableJoystickMovement();

        playrsBag.transform.SetParent(transform);
        _confettyParticle.Play();
        _raftRoot.transform.DOMove(_arivvedPoint.position, 30f);
        _sail.transform.DOScaleY(1, 5);
        SetNewCamera();
        _animator.Dance();
    }

    private void DisableJoystickMovement()
    {
        _playerRigidbody.isKinematic = true;

        _joystick.ClearInputValue();

        _joistickHandler.gameObject.SetActive(false);
    }

    private void SetNewCamera()
    {
        _mainCamera.Priority = 0;
        _levelCompleteCamera.Priority = 1;
    }
}
