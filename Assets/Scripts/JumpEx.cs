using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEx : MonoBehaviour {

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float _FallMultiplier = 1.5f;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float _LowFallMultiplier = 1.3f;

    private CharacterController _Controller;
    private bool _LowStartFalling = false;
    private bool _JumpButton;

    private void Awake()
    {
        _Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _JumpButton = Input.GetButton("Jump");


        if (_Controller.velocity.y < 0)
        {
            _Controller.Move(Vector3.up * Physics.gravity.y * (_FallMultiplier - 1.0f) * Time.deltaTime);
        }
        else if (_Controller.velocity.y > 0 && !_JumpButton && !_LowStartFalling)
        {
            _LowStartFalling = true;
        }

        if (_Controller.isGrounded)
        {
            _LowStartFalling = false;
        }

        if (_LowStartFalling)
        {
            _Controller.Move(Vector3.up * Physics.gravity.y * (_LowFallMultiplier - 1.0f) * Time.deltaTime);
        }
    }
}
