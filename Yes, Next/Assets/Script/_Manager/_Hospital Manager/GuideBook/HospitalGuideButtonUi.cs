using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HospitalGuideButtonUi : MonoBehaviour
{
    public GuideBookData _guideBookData;

    public TextMeshProUGUI _nameText;
    // public TextMeshProUGUI _descriptionText;

    public void AllocateData(GuideBookData guideBookData)
    {
        _guideBookData = guideBookData;
        _nameText.text = _guideBookData._name;
        // _descriptionText.text = _guideBookData._description;
    }

    public void DisplayRaceSymptom()
    {
        HospitalManager.Instance._hospitalGuideBook.DisplayRaceSymptom(_guideBookData);
    }
}
