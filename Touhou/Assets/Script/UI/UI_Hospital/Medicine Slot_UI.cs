using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MedicineSlot_UI : MonoBehaviour
{
    [SerializeField] private Image medicineSprite;
    [SerializeField] private TextMeshProUGUI medicineName;
    // [SerializeField] private TextMeshProUGUI medicineDescription;
    [SerializeField] private TextMeshProUGUI medicineAmount;
    public InventoryItemData itemData;
    [SerializeField] private InventorySystem medicineInventory;

    private void Awake() 
    {
    }

    public void AssignItem(InventoryItemData itemData)
    {
        this.itemData = itemData;
        medicineSprite.sprite = itemData.Icon;
        medicineName.text = itemData.DisplayName;
        // medicineDescription.text = itemData.Description;
        CheckItem();
    }

    public void CheckItem()
    {
        int amount = medicineInventory.GetItemCount(itemData);
        medicineAmount.text = amount.ToString();
    }

    public void buttonClicked()
    {
        HospitalManager.Instance.GiveMedicine(this.itemData);
    }
}
