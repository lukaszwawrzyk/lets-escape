using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovingPlatform : NetworkBehaviour
{

	[SerializeField] private Transform _start;
	[SerializeField] private Transform _end;
	[SerializeField] private Transform _platform;
	[SerializeField] private float _speed = 2.0f;
	[SerializeField] private float _grabbingBoxHeight = 2.5f;

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
		AddPlayerToPlatform(other);
	}

	private void AddPlayerToPlatform(Collision collider)
	{
		var player = collider.gameObject.GetComponent<CharacterController>();
		if (player != null)
		{
			_playersOnPlatform.Add(player);
		}
	}

	private void FixedUpdate()
	{
		var moveDistance = _speed * Time.fixedDeltaTime;
		var movementVector = _direction * moveDistance;

		RemovePlayersOutsideGrabbingBox();
		MovePlatform(movementVector);
		MovePlayers(movementVector);

		if (DistanceToDestination() < moveDistance)
		{
			ToggleDestination();
		}
	}

	private float DistanceToDestination()
	{
		return Vector3.Distance(_platform.position, _destination.position);
	}

	private void MovePlayers(Vector3 movementVector)
	{
		foreach (var player in _playersOnPlatform)
		{
			player.Move(movementVector);
		}
	}

	private void MovePlatform(Vector3 movementVector)
	{
		_platform.Translate(movementVector);
	}

	private void RemovePlayersOutsideGrabbingBox()
	{
		var platformBounds = GrabbingBoxBounds();
		_playersOnPlatform.RemoveWhere(player => !platformBounds.Contains(player.transform.position));
	}

	private Bounds GrabbingBoxBounds()
	{
		var yPos = _platform.position.y + (_grabbingBoxHeight + _platform.localScale.y) / 2.0f;
		var pos = new Vector3(_platform.position.x, yPos, _platform.position.z);
		var scale = new Vector3(_platform.localScale.x, _grabbingBoxHeight, _platform.localScale.z);
		return new Bounds(pos, scale);
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
