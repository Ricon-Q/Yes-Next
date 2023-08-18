// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class Slot : MonoBehaviour
// {
//     [SerializeField] Image image;

//     [SerializeField] private Item _item;
//     private Item itemBeingDragged;
//     private Vector3 startPosition;
//     private Transform startParent;

//     public Item item
//     {
//         get { return _item; }
//         set
//         {
//             _item = value;
//             if(_item != null)
//             {
//                 image.sprite = item.itemImage;
//                 image.color = new Color(1, 1, 1, 1);
//             }
//             else { image.color = new Color(1, 1, 1, 0); }
//         }
//     }
// }
