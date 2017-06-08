using UnityEngine;

public class Collectible : MonoBehaviour, IInteractiveWithPlayer
{
    private PlayerInventory _playerInventory;
    
    private void Start()
    {
        _playerInventory = GameObject.Find("common_inventory").GetComponent<PlayerInventory>();
    }

    public void Interact()
    {      
        _playerInventory.Add(this);
    }
}