using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
        else { Destroy(this.gameObject); }
    }

    private void Start()
    {
        SetupInventory();   
    }

    private void Update()
    {
        // Tab : 인벤토리 토글
        if (InputManager.Instance.GetToggleInventoryPressed() )
        {
            Debug.Log("ToogleInv");
            
            if(ShopManager.Instance.isShopMode) return;
            
            ToggleInventory();
        }
    }

    // Inventory
    public GameObject InventoryCanvas;
    public bool isInventoryOpen = false;
    public DynamicInventoryDisplay invToDisplay;
    private void SetupInventory()
    {
        InventoryCanvas.SetActive(false);
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        InventoryCanvas.SetActive(isInventoryOpen);
        invToDisplay.isInfoOpen = false;
        invToDisplay.infoPanel.SetActive(false);
    }
}

