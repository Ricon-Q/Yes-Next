using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Disease Data", menuName = "Hospital System/Disease Data")]
public class DiseaseData : ScriptableObject
{
    // 병명
    public string diseaseName;

    // 입원, 약 분류 (입원 환자일 경우 참)
    public bool isAdmission;

    // 약 환자일시 정답 약 (입원 환자일 경우 null)
    public InventoryItemData correctMedicine;

    // 심각도
    public int severity;

    // 평판
    public int reputation;

    // 수입
    public int income;

    // 대화
    public TextAsset dialogueText;
}
