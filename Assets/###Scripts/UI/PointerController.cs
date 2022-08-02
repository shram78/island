using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    [SerializeField] private Image _pointerImage;
    [SerializeField] private Transform _target;

    private Vector2 _pointerPosition;

    private void FixedUpdate()
    {
        _pointerPosition = Camera.main.WorldToScreenPoint(_target.position);
        _pointerPosition.x = Mathf.Clamp(_pointerPosition.x, 50.0f, Screen.width - 50f);
        _pointerPosition.y = Mathf.Clamp(_pointerPosition.y, 80.0f, Screen.height - 80f);

        _pointerImage.transform.position = _pointerPosition;
    }

    public void SetNewTarget(Transform target)
    {
        _target = target;
    }
}
