using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private Vector3 _spawnPoint = new Vector3(0.0f, 0.0f, 0.0f);

	public void spawn() {
		transform.position = _spawnPoint;
	}
}
