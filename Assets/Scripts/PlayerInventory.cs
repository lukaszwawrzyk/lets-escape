using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Collectible> _inventory = new List<Collectible>();

    public void Add(Collectible collectible)
    {
        _inventory.Add(collectible);
        collectible.GetComponent<Renderer>().enabled = false;
    }

    public bool HasKey(int key)
    {
        foreach (var item in _inventory)
        {
            if (item is Key)
            {
                var keyObj = item as Key;
                Debug.Log(keyObj._key);
                if (keyObj._key == key)
                {
                    return true;
                }
            }            
        }
        return false;
    }
}