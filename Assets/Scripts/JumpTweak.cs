using UnityEngine;

public class JumpTweak : MonoBehaviour {

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float _fallMultiplier = 1.5f;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float _lowFallMultiplier = 1.3f;

    private CharacterController _controller;
    private bool _lowStartFalling;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var jumpButtonClicked = Input.GetButton("Jump");

        if (_controller.velocity.y < 0)
        {
            _controller.Move(Vector3.up * Physics.gravity.y * (_fallMultiplier - 1.0f) * Time.deltaTime);
        }
        else if (_controller.velocity.y > 0 && !jumpButtonClicked && !_lowStartFalling)
        {
            _lowStartFalling = true;
        }

        if (_controller.isGrounded)
        {
            _lowStartFalling = false;
        }

        if (_lowStartFalling)
        {
            _controller.Move(Vector3.up * Physics.gravity.y * (_lowFallMultiplier - 1.0f) * Time.deltaTime);
        }
    }
}
