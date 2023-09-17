// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Runtime.Serialization.Formatters.Binary;
// using System.IO;
// using UnityEditor;
// using System.Runtime.Serialization;

// [CreateAssetMenu(fileName = "New Inventory", menuName = "InventoryTest/Inventory")]
// [System.Serializable]
// public class InventoryObject : ScriptableObject
// {
//     public string savePath;
//     public ItemDatabaseObject database;
//     public Inventory Container;
//     public Inventory test;


//     public void AddItem(Item _item, int _amount)
//     {
//         // 아이템이 Countable = false일 경우
//         if(!_item.Countable)
//         {
//             // Debug.Log("Cant count");
//             SetEmptySlot(_item, _amount);
//             return;
//         }
//         // 아이템이 Countable = true일 경우
//         for(int i = 0; i < Container.Items.Length; i++)
//         {
//             if(Container.Items[i].ID == _item.Id)
//             {
//                 // 이미 있는 아이템일 경우 갯수 추가
//                 Container.Items[i].AddAmount(_amount);
//                 return;
//             }
//         }
//         SetEmptySlot(_item, _amount);
//     }
//     public InventorySlot SetEmptySlot(Item _item, int _amount)
//     {
//         for (int i = 0; i < Container.Items.Length; i++)
//         {
//             if(Container.Items[i].ID <= -1)
//             {
//                 Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
//                 return Container.Items[i];
//             }
//         }

//         // set up functionallity for full inventory
//         return null;
//     }

//     [ContextMenu("Save")]
//     public void Save()
//     {
//         string saveData = JsonUtility.ToJson(this, true);
//         BinaryFormatter bf = new BinaryFormatter();
//         FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
//         bf.Serialize(file, saveData);
//         file.Close();
//     }

//     [ContextMenu("Load")]
//     public void Load()
//     {
//         if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
//         {
//             BinaryFormatter bf = new BinaryFormatter();
//             FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
//             JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
//             file.Close();
//         }
//     }

//     [ContextMenu("Clear")]
//     public void Clear()
//     {
//         Container = new Inventory();
//     }

//     public void MoveItem(InventorySlot item1, InventorySlot item2)
//     {
//         InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
//         item2.UpdateSlot(item1.ID, item1.item, item1.amount);
//         item1.UpdateSlot(temp.ID, temp.item, temp.amount);
//     }
//     public void RemoveItem(Item _item, int amount = 1)
//     {
//         for (int i = 0; i < Container.Items.Length; i++)
//         {
//             if(Container.Items[i].item == _item)
//             {
//                 Container.Items[i].amount -= amount;

//                 if(Container.Items[i].amount <= 0)
//                 {
//                     Container.Items[i].UpdateSlot(-1, null, 0);
//                 }
//             }
//         }
//     }
// }

// [System.Serializable]
// public class Inventory
// {
//     public InventorySlot[] Items = new InventorySlot[15];
// }

// [System.Serializable]
// public class InventorySlot
// {
//     public int ID = -1;
//     public Item item;
//     public int amount;
//     public InventorySlot()
//     {
//         ID = -1;
//         item = null;
//         amount = 0;
//     }

//     public InventorySlot(int _id, Item _item, int _amount)
//     {
//         ID = _id;
//         item = _item;
//         amount = _amount;
//     }

//     public void UpdateSlot(int _id, Item _item, int _amount)
//     {
//         ID = _id;
//         item = _item;
//         amount = _amount;
//     }

//     public void AddAmount(int value)
//     {
//         amount += value;
//     }
// }
