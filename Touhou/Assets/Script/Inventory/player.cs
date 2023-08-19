using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public InventoryObject inventory;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            Debug.Log("Saved");
        }    
        if(Input.GetKeyDown(KeyCode.A))
        {
            inventory.Load();
            
            Debug.Log("Loaded");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if(item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit() 
    {
        inventory.Container.Items = new InventorySlot[15];
    }
}
