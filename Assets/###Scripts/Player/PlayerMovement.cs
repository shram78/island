using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _viewToRotate;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (_joystick.Direction == Vector2.zero)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y), Vector3.up) * Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);

        _rigidbody.MovePosition(_rigidbody.position + (transform.forward * _moveSpeed * Time.fixedDeltaTime));

        //_rigidbody.velocity = (transform.forward * _joystick.Vertical + _joystick.Horizontal * transform.right) * _moveSpeed;

        //Rotate();
    }

    private void Rotate()
    {
        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(_rigidbody.velocity);
            lookRotation.x = 0;
            lookRotation.z = 0;

            _viewToRotate.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
