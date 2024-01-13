using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _InventoryManager : MonoBehaviour
{
    private static _InventoryManager instance;
    public static _InventoryManager Instance
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

    // ============================================================= //

    [Header("Inventory")]
    [SerializeField] private GameObject InventoryCanvas;
    private bool isInventoryOpen = false;

    private void Start() 
    {
        InventoryCanvas.SetActive(false);
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        InventoryCanvas.SetActive(isInventoryOpen);
    }
}
