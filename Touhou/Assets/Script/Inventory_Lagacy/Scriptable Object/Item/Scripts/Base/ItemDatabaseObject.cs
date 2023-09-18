// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(fileName = "New Item Database", menuName = "InventoryTest/Items/DataBase")]
// [System.Serializable]
// public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
// {
//     // Database
    
//     public ItemObject[] Items;
//     public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

//     public void OnAfterDeserialize()
//     {
//         for (int i = 0; i < Items.Length; i++)
//         {
//             Items[i].Id = i;
//             GetItem.Add(i, Items[i]);
//         }
//     }

//     public void OnBeforeSerialize()
//     {
//         GetItem = new Dictionary<int, ItemObject>();
//     }
// }