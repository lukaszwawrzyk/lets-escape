using UnityEngine;
using UnityEngine.Networking;

public class Button : NetworkBehaviour, IInteractiveWithPlayer
{
    [SerializeField] private Door _target;

    [ClientRpc]
    public void Interact()
    {
        _target.Clicked();
    }
}