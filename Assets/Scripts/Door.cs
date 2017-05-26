using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ButtonTarget {

    [SerializeField]
    private float angle = 0;

    [SerializeField]
    private float openAngle = 130.0f;

    [SerializeField]
    private float openingSpeed = 150.0f;

    private bool opening = false;
    private bool closing = false;

    private bool opened = false;

    void Update () {
        if (opening)
        {
            angle += Time.deltaTime * openingSpeed;

            if (angle >= openAngle)
            { 
                opening = false;
                angle = openAngle;
            }
        }
        else if (closing)
        {
            angle -= Time.deltaTime * openingSpeed;

            if (angle <= 0)
            {
                closing = false;
                angle = 0;
            }
        }

        transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    void Open()
    {
        opened = true;
        opening = true;
    }

    void Close()
    {
        opened = false;
        closing = true;
    }

    public void Clicked()
    {
        if (opened)
        {
            Close();
        }
        else
        {
            Open();
        }
        
    }
}
