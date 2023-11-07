using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patient Data", menuName = "Hospital System/Patient/Patient Data")]
public class PatientData : ScriptableObject

{
    // 환자 ID
    // public int patientID = -1;

    // 환자 이름
    public string patientName;

    // 환자 스프라이트
    public Sprite[] standingCG;

    // 환자 질병 데이터
    public DiseaseData diseaseData;
}
