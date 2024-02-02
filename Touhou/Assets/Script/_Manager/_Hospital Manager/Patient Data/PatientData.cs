using UnityEngine;


[CreateAssetMenu(fileName = "New Patient Data", menuName = "Hospital System/Patient Data")]
public class PatientData : ScriptableObject
{
    [Header("Patient Data")]
    // 환자 이름
    public string _name;
    // 종족
    
    [Header("Conversation")]
    public string _conversationTitle;

    [Header("Disase")]
    // 질병 종류
    
    // 포션 데이터
    public InventoryItemData _potionItemData;
}
