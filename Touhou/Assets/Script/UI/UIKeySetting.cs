using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player 오브젝트에 부착. UI 기능을 위한 키 바인딩

public class UIKeySetting : MonoBehaviour
{
    private void Start()
    {
        SetupInventory();   
    }

    private void Update()
    {
        // Tab : 인벤토리 토글
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

    }

    // Inventory
    public GameObject InventoryCanvas;
    private bool isInventoryOpen = false;
    
    private void SetupInventory()
    {
        InventoryCanvas.SetActive(false);
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        InventoryCanvas.SetActive(isInventoryOpen);
    }
    
}
