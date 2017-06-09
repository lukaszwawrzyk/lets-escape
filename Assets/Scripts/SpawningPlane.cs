using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPlane : MonoBehaviour {

	protected void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        player.spawn();
	}
}
