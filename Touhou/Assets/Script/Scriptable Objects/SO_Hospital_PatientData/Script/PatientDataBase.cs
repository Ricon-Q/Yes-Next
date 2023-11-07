using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Patient Database", menuName = "Hospital System/Patient/Patient DataBase")]
[System.Serializable]
public class PatientDataBase : ScriptableObject
{
    public PatientData[] Items = new PatientData[0];
    // public Dictionary<int, PatientData> GetItem = new Dictionary<int, PatientData>();

    // public void OnAfterDeserialize()
    // {
    //     for (int i = 0; i < Items.Length; i++)
    //     {
    //         Items[i].patientID = i;
    //         GetItem.Add(i, Items[i]);
    //     }
    // }

    // public void OnBeforeSerialize()
    // {
    //     GetItem = new Dictionary<int, PatientData>();
    // }
}
