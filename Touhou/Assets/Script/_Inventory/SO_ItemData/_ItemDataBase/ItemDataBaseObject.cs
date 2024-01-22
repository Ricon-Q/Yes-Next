using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Inventory Item/DataBase")]
[System.Serializable]
// public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
public class ItemDatabaseObject : ScriptableObject
{
    public InventoryItemData[] Items;

    // public void OnAfterDeserialize()
    // {
    //     // This method is called after the object is deserialized.
    //     // Here you can set the ID of each InventoryItemData to its index.
    //     for (int i = 0; i < Items.Length; i++)
    //     {
    //         if (Items[i] != null)
    //             Items[i].ID = i;
    //     }
    // }
    
    [ContextMenu("Update ItemDatabase")]
    public void UpdateItemDatabase()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
                Items[i].ID = i;
        }
    }
}