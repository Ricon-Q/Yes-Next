 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patient Database", menuName = "Hospital System/Patient Database")]
public class PatientDatabase : ScriptableObject
{
    public List<PatientData> _patientDatas;

    public List<PatientData> GetRandomPatientData()
    {
        List<PatientData> randomPatientData = new List<PatientData>();

        List<PatientData> copyOfPatientData = new List<PatientData>(_patientDatas);

        int i = 0;
        while(i < _patientDatas.Count)
        {
            if (copyOfPatientData.Count == 0)
            {
                break;  // 사용 가능한 퀘스트가 더 이상 없음
            }

            int randomIndex = Random.Range(0, copyOfPatientData.Count);

            randomPatientData.Add(copyOfPatientData[randomIndex]);
            copyOfPatientData.RemoveAt(randomIndex);
            i++;
        }
        return randomPatientData;
    }
}
