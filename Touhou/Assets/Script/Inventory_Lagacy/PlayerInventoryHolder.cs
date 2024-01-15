using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    // [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem backpackInventorySystem;
    public InventorySystem BackpackInventorySystem => backpackInventorySystem;

    [SerializeField] protected InventorySystem medicalInventorySystem;
    public InventorySystem MedicalInventorySystem => medicalInventorySystem;

    public StaticInventoryDisplay hotBarDisplay;
    public DynamicInventoryDisplay backpackDisplay;
    
    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    private static PlayerInventoryHolder instance;

    public static PlayerInventoryHolder Instance
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

    protected override void Awake()
    {
        base.Awake();
        if(instance == null) { instance = this; }
        else { Destroy(this.gameObject); }
    }
    void Update()
    {
        if(Keyboard.current.bKey.wasPressedThisFrame) OnPlayerBackpackDisplayRequested?.Invoke(BackpackInventorySystem);

        if(Keyboard.current.sKey.wasPressedThisFrame) 
        {
            MedicalInventorySystem.Save();
            BackpackInventorySystem.Save();
        }
        if(Keyboard.current.lKey.wasPressedThisFrame) 
        {
            MedicalInventorySystem.Load();
            hotBarDisplay.UpdateSlot();
            BackpackInventorySystem.Load();
            backpackDisplay.RefreshDynamicInventory(BackpackInventorySystem);
        }
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if(data.ItemType == ItemType.Ingredient || data.ItemType == ItemType.Medicine)
        {
            if(medicalInventorySystem.AddToInventory(data, amount)) 
            {
                return true;
            }
        }
        else if(BackpackInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        return false;
    }
}
