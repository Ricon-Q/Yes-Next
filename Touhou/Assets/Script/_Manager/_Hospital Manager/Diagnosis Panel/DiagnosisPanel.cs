using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiagnosisPanel : MonoBehaviour
{
    [Header("Patient Data")]
    [SerializeField] private PatientData _allocatedPatientData;

    [Header("Diagnosis")]
    [SerializeField] private TMP_Dropdown _disaseDropdown;
    
    public void AllocatePatientData(PatientData patientData)
    {
        _allocatedPatientData = patientData;
    }

    public void test()
    {
        Debug.Log(_disaseDropdown.options[_disaseDropdown.value].text);
    }
}
