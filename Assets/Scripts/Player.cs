using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Control")]

    [SerializeField]
    [Range(0.1f, 25.0f)]
    private float _Speed = 5.0f;

    [Header("Jumps")]

    [SerializeField]
    [Range(0.1f, 50.0f)]
    private float _JumpVelocity = 7.0f;

    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float _Gravity = 9.81f;

    [SerializeField]
    private bool _AirControll = true;

    
    private Vector3 _MoveDirection = Vector3.zero;
    private float _InputX;
    private float _InputY;
    private bool _Jump;
    private float _MouseX;
    private float _MouseY;

    void Update() {
        _InputX = Input.GetAxis("Horizontal");
        _InputY = Input.GetAxis("Vertical");
        _Jump = Input.GetButton("Jump");

        var controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            _MoveDirection = new Vector3(_InputX, 0, _InputY);
            _MoveDirection = transform.TransformDirection(_MoveDirection);
            _MoveDirection *= _Speed;
            if (_Jump)
            {
                _MoveDirection.y = _JumpVelocity;
            }

        }
        else
        {
            if (_AirControll)
            {
                _MoveDirection.x = _InputX * _Speed;
                _MoveDirection.z = _InputY * _Speed;
                _MoveDirection = transform.TransformDirection(_MoveDirection);
            }
        }

        _MoveDirection.y -= _Gravity * Time.deltaTime;
        controller.Move(_MoveDirection * Time.deltaTime);
    }
}

