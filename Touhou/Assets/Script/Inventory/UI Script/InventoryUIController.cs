// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class InventoryUIController : MonoBehaviour
// {
//     public DynamicInventoryDisplay chestPanel;
//     public DynamicInventoryDisplay playerBackpackPanel;

//     private void Awake() 
//     {
//         playerBackpackPanel.gameObject.SetActive(false);
//         chestPanel.gameObject.SetActive(false);
//     }

//     private void OnEnable() 
//     {
//         InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;    
//         PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
//     }
//     private void OnDisable()
//     {
//         InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;    
//         PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
//     }

//     void Update()
//     {
//         if(chestPanel.gameObject.activeInHierarchy && Keyboard.current.tabKey.wasPressedThisFrame) 
//             { chestPanel.gameObject.SetActive(false); }

//         if(playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.tabKey.wasPressedThisFrame) 
//             { playerBackpackPanel.gameObject.SetActive(false); }
//     }

//     void DisplayInventory(InventorySystem invToDisplay)
//     {
//         chestPanel.gameObject.SetActive(true);
//         chestPanel.RefreshDynamicInventory(invToDisplay);
//     }

//     void DisplayPlayerBackpack(InventorySystem invToDisplay)
//     {
//         playerBackpackPanel.gameObject.SetActive(true);
//         playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
//     }
// }
