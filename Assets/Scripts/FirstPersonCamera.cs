using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    [SerializeField]
    private float _sensitivity = 5.0f;

    private GameObject _player;
    private Vector2 _lookDirection;
    private bool _cameraControlEnabled = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _player = transform.parent.gameObject;
    }

    private void Update()
    {
        if (_cameraControlEnabled)
        {
            RotateCamera();
        }

        if (Input.GetKeyDown("escape"))
        {
            ToggleCameraControl();
        }
    }

    private void RotateCamera()
    {
        var mousePosition = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var mouseDelta = Vector2.Scale(mousePosition, new Vector2(_sensitivity, _sensitivity));
        _lookDirection += mouseDelta;

        transform.localRotation = Quaternion.AngleAxis(-_lookDirection.y, Vector3.right);
        _player.transform.localRotation = Quaternion.AngleAxis(_lookDirection.x, _player.transform.up);
    }

    private void ToggleCameraControl()
    {
        if (_cameraControlEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            _cameraControlEnabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            _cameraControlEnabled = true;
        }
    }
}
