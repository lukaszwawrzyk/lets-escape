using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Networking;

public class MovingPlatform : NetworkBehaviour
{

	[SerializeField] private Transform _start;
	[SerializeField] private Transform _end;
	[SerializeField] private Transform _platform;
	[SerializeField] private float _speed = 2.0f;

	private Vector3 _direction;
	private Transform _destination;
	private HashSet<CharacterController> _playersOnPlatform;

	private void Start()
	{
		_playersOnPlatform = new HashSet<CharacterController>();
		SetDestination(_end);
	}


	private void OnCollisionStay(Collision other)
	{
		var player = other.gameObject.GetComponent<CharacterController>();
		if (player != null)
		{
			_playersOnPlatform.Add(player);
		}
	}

	private void FixedUpdate()
	{
		var moveDistance = _speed * Time.fixedDeltaTime;
		var movementVector = _direction * moveDistance;

		const float boxHeight = 2.5f;
		var pos = new Vector3(_platform.position.x, _platform.position.y + (boxHeight + _platform.localScale.y) / 2.0f, _platform.position.z);
		var scale = new Vector3(_platform.localScale.x, boxHeight, _platform.localScale.z);
		var platformBounds = new Bounds(pos, scale);
		_playersOnPlatform.RemoveWhere(player => !platformBounds.Contains(player.transform.position));
		_platform.Translate(movementVector);
		foreach (var player in _playersOnPlatform)
		{
			player.Move(movementVector);
		}

		if (Vector3.Distance(_platform.position, _destination.position) < moveDistance)
		{
			ToggleDestination();
		}
	}

	private void ToggleDestination()
	{
		SetDestination(_destination == _start ? _end : _start);
	}

	private void SetDestination(Transform destination)
	{
		_destination = destination;
		_direction = (_destination.position - _platform.position).normalized;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(_start.position, _platform.localScale);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(_end.position, _platform.localScale);
	}
}
