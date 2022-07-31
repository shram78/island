using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Events;
using System.Collections;

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
    [SerializeField] private GameObject _UILetsGo;
    [SerializeField] private GameSceneManager _gameSceneManager;

    public UnityAction Comleted;

    private bool _isFoofCollected = false;

    public void SetFoodCollected()
    {
        _isFoofCollected = true;
        _UIFood.gameObject.SetActive(false);
        _UILetsGo.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isFoofCollected)
        {
            if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
                LeaveTheIsland(playrsBag);
        }
    }

    private void LeaveTheIsland(PlayrsBag playrsBag)
    {
        Comleted?.Invoke();

        DisableJoystickMovement();

        _UILetsGo.gameObject.SetActive(false);
        playrsBag.transform.SetParent(transform);
        _confettyParticle.Play();
        _raftRoot.transform.DOMove(_arivvedPoint.position, 30f);
        _sail.transform.DOScaleY(1, 10);
        SetNewCamera();
        _animator.Dance();

        StartCoroutine(LoadNextLevelOnTimer());
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

    private IEnumerator LoadNextLevelOnTimer()
    {
        yield return new WaitForSeconds(10f);

        _gameSceneManager.LoadNextScene();
    }
}
