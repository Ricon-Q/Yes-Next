// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI; // 추가

// public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     private RectTransform rectTransform;
//     private CanvasGroup canvasGroup;

//     private Slot parentSlot;

//     private void Awake()
//     {
//         rectTransform = GetComponent<RectTransform>();
//         canvasGroup = GetComponent<CanvasGroup>();
//         parentSlot = GetComponentInParent<Slot>();
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         // When dragging starts, make the item partially transparent
//         canvasGroup.alpha = 0.6f;
//         canvasGroup.blocksRaycasts = false;

//         // Detach the item from its current parent
//         transform.SetParent(transform.root);
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         // Update the item's position while dragging
//         rectTransform.anchoredPosition += eventData.delta / parentSlot.GetComponent<CanvasScaler>().scaleFactor;
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         // When dragging ends, return the item to its parent slot
//         canvasGroup.alpha = 1f;
//         canvasGroup.blocksRaycasts = true;

//         transform.SetParent(parentSlot.transform);
//         transform.localPosition = Vector3.zero;
//     }
// }