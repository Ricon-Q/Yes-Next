using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    // [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public StaticInventoryDisplay hotBarDisplay;
    public DynamicInventoryDisplay backpackDisplay;
    
    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    protected override void Awake()
    {
        base.Awake();
        // secondaryInventorySystem = new InventorySystem(SecondaryInventorySystem.InventorySize);
    }
    void Update()
    {
        if(Keyboard.current.bKey.wasPressedThisFrame) OnPlayerBackpackDisplayRequested?.Invoke(SecondaryInventorySystem);

        if(Keyboard.current.sKey.wasPressedThisFrame) 
        {
            PrimaryInventorySystem.Save();
            SecondaryInventorySystem.Save();
        }
        if(Keyboard.current.lKey.wasPressedThisFrame) 
        {
            PrimaryInventorySystem.Load();
            hotBarDisplay.UpdateSlot();
            SecondaryInventorySystem.Load();
            backpackDisplay.RefreshDynamicInventory(SecondaryInventorySystem);
        }
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if(primaryInventorySystem.AddToInventory(data, amount)) 
        {
            return true;
        }
        else if(secondaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        return false;
    }
}
