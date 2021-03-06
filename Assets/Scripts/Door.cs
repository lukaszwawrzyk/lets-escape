﻿using UnityEngine;
using UnityEngine.Networking;

public class Door : NetworkBehaviour, IButtonTarget
{
    [SerializeField] private float _angle;
    [SerializeField] private float _openAngle = 130.0f;
    [SerializeField] private float _openingSpeed = 150.0f;

    [SerializeField] private bool _locked = false;
    [SerializeField] private int _lockKey = 0;

    public bool _opening = false;
    public bool _closing = false;
    
    [SyncVar]
    public bool _opened = false;
    
    private PlayerInventory _playerInventory;


    private void Start()
    {
        _playerInventory = GameObject.Find("common_inventory").GetComponent<PlayerInventory>();
    }
    
    private void Update()
    {
        if (_opened)
        {
            _opening = true;
            _closing = false;
        }
        else
        {
            _opening = false;
            _closing = true;
        }
        
        if (_opening)
        {
            Debug.Log("opening!");
            _angle += Time.deltaTime * _openingSpeed;

            if (_angle >= _openAngle)
            {
                _opening = false;
                _angle = _openAngle;
            }
        }
        else if (_closing)
        {
            _angle -= Time.deltaTime * _openingSpeed;

            if (_angle <= 0)
            {
                _closing = false;
                _angle = 0;
            }
        }

        transform.localRotation = Quaternion.Euler(new Vector3(0, _angle, 0));
    }

    private void Open()
    {
        if (!_locked)
        {
            _opened = true;
            Debug.Log("Open");
            /*if (isLocalPlayer)
            {
                CmdUpdateOpened(true);
            }*/
        }
        else
        {
            if (_playerInventory.HasKey(_lockKey))
            {
                _opened = true;
                Debug.Log("Open");
                /*if (isLocalPlayer)
                {
                    CmdUpdateOpened(true);
                }*/
            }
        }
    }

    private void Close()
    {
        _opened = false;
        Debug.Log("Close");
        /*if (isLocalPlayer)
        {
            CmdUpdateOpened(false);
        }*/
    }

    /*[Command]
    void CmdUpdateOpened(bool opened)
    {
        Debug.Log("Command!");
        _opened = opened;
    }*/
    
    public void Clicked()
    {
        if (_opened)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}