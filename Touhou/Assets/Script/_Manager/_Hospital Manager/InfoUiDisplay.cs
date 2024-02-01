using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUiDisplay : MonoBehaviour
{
    [Header("Time Display")]
    [SerializeField] private TextMeshProUGUI _timeDisplayText;

    [Header("Button Grid")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _endHospitalMode;

    public void EnterHospitalMode()
    {
        RefreshTimeDisplay();
    }

    public void ExitHospitalMode()
    {

    }

    public void RefreshTimeDisplay()
    {
        int _hour = _TimeManager.Instance.timeData.hour;
        int _minute = _TimeManager.Instance.timeData.minute;
        if(_hour > 12)
            _timeDisplayText.text = string.Format("오후 {0}시 {1}분", _hour-12, _minute);
        else if(_hour == 12)
            _timeDisplayText.text = string.Format("오후 {0}시 {1}분", _hour, _minute);
        else
            _timeDisplayText.text = string.Format("오전 {0}시 {1}분", _hour, _minute);
    }           
}