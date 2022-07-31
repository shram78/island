using System.Collections;
using Cinemachine;
using UnityEngine;

public class ShipDelivery : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playCamera;
    [SerializeField] private CinemachineVirtualCamera _shipDeliveryCamera;
    [SerializeField] private Zone _lightHouse;
    [SerializeField] private GameObject _joistickHandler;
    [SerializeField] private FloatingJoystick _floatingJoystick;

    private void OnEnable()
    {
        _lightHouse.Opened += OnSetNewCamera;
    }

    private void Start()
    {
        _playCamera.Priority = 1;
    }

    private void OnDisable()
    {
        _lightHouse.Opened -= OnSetNewCamera;
    }

    private void OnSetNewCamera()
    {
        StartCoroutine(MakePause());
    }

    private IEnumerator MakePause()
    {
        yield return new WaitForSeconds(1);

        _playCamera.Priority = 0;
        _shipDeliveryCamera.Priority = 1;

        _floatingJoystick.ClearInputValue();

        _joistickHandler.gameObject.SetActive(false);

        StartCoroutine(ShowOnTimer(_shipDeliveryCamera));
    }

    private IEnumerator ShowOnTimer(CinemachineVirtualCamera currentCamera)
    {
        yield return new WaitForSeconds(5);

        SetMainCamera(currentCamera);
        _joistickHandler.gameObject.SetActive(true);
    }

    private void SetMainCamera(CinemachineVirtualCamera currentCamera)
    {
        currentCamera.Priority = 0;
        _playCamera.Priority = 1;
    }

}
