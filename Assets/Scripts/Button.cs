using UnityEngine;
using UnityEngine.Networking;

public class Button : MonoBehaviour, IInteractiveWithPlayer
{
    [SerializeField] private Door _target;

    public void Interact()
    {
        _target.Clicked();
    }
}