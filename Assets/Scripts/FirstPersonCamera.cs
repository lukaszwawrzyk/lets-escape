using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    private GameObject _Player;
    private Vector2 mouseLook;

    [SerializeField]
    private float _Sensitivity = 5.0f;

    private bool _EnableControll = true;
    private Vector2 _MouseDelta;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _Player = this.transform.parent.gameObject;
    }

    private void Update()
    {
        _MouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        if (_EnableControll)
        {
            _MouseDelta = Vector2.Scale(_MouseDelta, new Vector2(_Sensitivity, _Sensitivity));

            mouseLook += _MouseDelta;

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            _Player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, _Player.transform.up);
        }

        if (Input.GetKeyDown("escape"))
        {
            if (_EnableControll)
            {
                Cursor.lockState = CursorLockMode.None;
                _EnableControll = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                _EnableControll = true;
            }

        }
    }

}
