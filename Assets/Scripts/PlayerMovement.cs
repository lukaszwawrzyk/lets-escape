using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Control")]

    [SerializeField]
    [Range(0.1f, 25.0f)]
    private float _speed = 5.0f;

    [Header("Jumps")]

    [SerializeField]
    [Range(0.1f, 50.0f)]
    private float _jumpVelocity = 7.0f;

    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float _gravity = 9.81f;

    [SerializeField]
    private bool _allowMovingInTheAir = true;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update() {
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");
        var jump = Input.GetButton("Jump");

        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(inputX, 0, inputY);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= _speed;
            if (jump)
            {
                _moveDirection.y = _jumpVelocity;
            }
        }
        else if (_allowMovingInTheAir)
        {
            _moveDirection.x = inputX * _speed;
            _moveDirection.z = inputY * _speed;
            _moveDirection = transform.TransformDirection(_moveDirection);
        }

        _moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }
}

