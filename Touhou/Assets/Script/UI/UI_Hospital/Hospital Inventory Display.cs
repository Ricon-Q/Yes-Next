using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalInventoryDisplay : MonoBehaviour
{
    /*/ 
        Hospital Manager에서 'Prescribe' 명령어 실행시 
        Hospital Manager의 ToggleHospitalInventoryDisplay 통해서
        Medicine 카테고리의 아이템을 보여준다
        플레이어는 그중에서 환자에게 맞는 약 처방
    /*/

    public InventorySystem MedicineInventorySystem;
    public bool isDisplayOpen;
    public ItemDatabaseObject medicineDataBase;
    public List<MedicineSlot_UI> medicineSlotList;
    public List<GameObject> medicineSlotObject;
    private void Awake() 
    { 
    }

    public void Display()
    {
        int index = 0;
        // Inventory의 Medicine 카테고리의 아이템들을 SlotList에 할당 후 인벤토리 보여주기
        foreach (var item in MedicineInventorySystem.InventorySlots)
        {
            if(item.ItemData != null)
            {
                medicineSlotList[index++].AssignItem(item.ItemData);
                medicineSlotObject[index].SetActive(true);
            }
            else
                continue;
        }
        for(int i = index; i < medicineSlotList.Count; i++)
        {
            medicineSlotObject[i].SetActive(false);
        }
        
    }

    public void UpdateList()
    {
        foreach (var item in medicineSlotList)
        {
            item.CheckItem();
        }
    }
}
