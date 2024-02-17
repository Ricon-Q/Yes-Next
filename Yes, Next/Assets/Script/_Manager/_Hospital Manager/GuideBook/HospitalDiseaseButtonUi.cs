using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HospitalDiseaseButtonUi : MonoBehaviour
{
    public DiseaseData _diseaseData;

    public TextMeshProUGUI _nameText;
    // public TextMeshProUGUI _descriptionText;

    public void AllocateData(DiseaseData diseaseData)
    {
        _diseaseData = diseaseData;
        _nameText.text = _diseaseData._name;
        // _descriptionText.text = _guideBookData._description;
    }

    public void DisplayRaceSymptom()
    {
        HospitalManager.Instance._hospitalGuideBook.DisplayDisease(_diseaseData);
    }
}
