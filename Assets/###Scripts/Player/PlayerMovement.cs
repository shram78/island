using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _viewToRotate;

    private Camera _camera;
    private float _rotateDegrees;
    private Quaternion _targetRotation;

    private Vector3 _lookDirection => new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y);

    private void Start()
    {
        _camera = Camera.main;
        _rotateDegrees = _camera.transform.rotation.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        if (_joystick.Direction == Vector2.zero)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        transform.rotation = Quaternion.LookRotation(_lookDirection, Vector3.up) * Quaternion.Euler(0, _rotateDegrees, 0);

        _rigidbody.MovePosition(_rigidbody.position + (transform.forward * (Mathf.Abs(_joystick.Vertical) + Mathf.Abs(_joystick.Horizontal)) * _moveSpeed * Time.fixedDeltaTime));
    }
}
