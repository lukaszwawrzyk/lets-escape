using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    private Door target;

    public bool click = false;

	void Start () {
		
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            var distance = (Camera.main.transform.position - target.transform.position).magnitude;
            if(distance < 5.0)
            {
                target.Clicked();
            }
        }
	}
}
