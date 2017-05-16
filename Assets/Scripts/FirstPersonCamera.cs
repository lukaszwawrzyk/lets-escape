using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    [SerializeField]
    private float _sensitivity = 5.0f;

    private Transform _player;
    private Transform _mainCamera;
    private Vector2 _lookDirection;
    private bool _cameraControlEnabled;

    private void Start()
    {
        _player = transform;
        _mainCamera = Camera.main.transform;
        EnableCameraMovement();
    }

    private void Update()
    {

        if (_cameraControlEnabled)
        {
            RotateCameraAndPlayer();
        }

        if (Input.GetKeyDown("escape"))
        {
            ToggleCameraControl();
        }

    }

    private void RotateCameraAndPlayer()
    {
        UpdateLookDirection();
        _player.rotation = Quaternion.AngleAxis(_lookDirection.x, _player.transform.up);
        _mainCamera.position = _player.position;
        _mainCamera.rotation = _player.rotation * Quaternion.AngleAxis(-_lookDirection.y, Vector3.right);
    }

    private void UpdateLookDirection()
    {
        var mousePosition = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var mouseDelta = Vector2.Scale(mousePosition, new Vector2(_sensitivity, _sensitivity));
        _lookDirection += mouseDelta;
    }

    private void ToggleCameraControl()
    {
        if (_cameraControlEnabled)
        {
            DisableCameraMovement();
        }
        else
        {
            EnableCameraMovement();
        }
    }

    private void EnableCameraMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraControlEnabled = true;
    }

    private void DisableCameraMovement()
    {
        Cursor.lockState = CursorLockMode.None;
        _cameraControlEnabled = false;
    }
}
