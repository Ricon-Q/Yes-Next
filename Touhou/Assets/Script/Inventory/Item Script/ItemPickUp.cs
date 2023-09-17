using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;
    private BoxCollider myCollider;

    private void Awake() 
    {
        myCollider = GetComponent<BoxCollider>();
        myCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
        if(!inventory) return;
        if(inventory.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }    
    }
}
