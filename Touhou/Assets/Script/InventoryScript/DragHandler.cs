// using System.Collections;
// using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     public static DragHandler instance;

//     private Item itemBeingDragged;
//     private Vector3 startPosition;
//     private Transform startParent;

//     // private CanvasGroup canvasGroup;
    
//     private void Awake()
//     {
//         instance = this;
//     }
    
//     private void Start()
//     {
//         // canvasGroup = GetComponent<CanvasGroup>();
//     }

//     public void StartDragging(Item item)
//     {
//         itemBeingDragged = item;
//         startPosition = transform.position;
//         startParent = transform.parent;
//         transform.SetParent(transform.parent.parent); // Place the dragged item above other UI elements
        
//         // canvasGroup.blocksRaycasts = false; // Ignore mouse events while dragging
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         if (itemBeingDragged != null)
//         {
//             transform.SetParent(transform.parent.parent); // Place the dragged item above other UI elements
//         }
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         if (itemBeingDragged != null)
//         {
//             // Update the position of the dragged item's image to match the mouse position
//             transform.position = Input.mousePosition;
//             // itemBeingDragged.itemImage.rectTransform.position = Input.mousePosition;
//         }
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
        
//         if (itemBeingDragged != null)
//         {
//             transform.position = startPosition;
//             itemBeingDragged = null;
//         }
//     }
// }
