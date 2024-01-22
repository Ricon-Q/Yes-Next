// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Inventory Item/DataBase")]
// [System.Serializable]
// // public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
// public class ItemDatabaseObject : ScriptableObject
// {
//     public InventoryItemData[] Items;
    
//     public void OnAfterDeserialize()
//     {
//         for (int i = 0; i < Items.Length; i++)
//         {
//             if (Items[i] != null)
//                 Items[i].ID = i;
//         }
//     }

//     public
// }