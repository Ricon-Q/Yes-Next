using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText; // 이 Text UI 요소는 인스펙터에서 연결해주어야 합니다.

    public int minuteDisplay;
    public int hourDisplay;
    public int dayDisplay;

    private TimeManager timeManager;

    private void Start()
    {
        timeManager = TimeManager.Instance;
    }

    private void UpdateTimeDisplay()
    {
        if (timeManager != null)
        {
            // 시간 데이터를 가져와서 Text UI 요소에 표시합니다.

            minuteDisplay = timeManager.timeData.minute;
            hourDisplay = timeManager.timeData.hour;
            dayDisplay = timeManager.timeData.day;
            string timeString = $"{dayDisplay}일차 {hourDisplay}시 {minuteDisplay}분";
            timeText.text = timeString;
        }
    }

    private void Update()
    {
        UpdateTimeDisplay();
        // Debug.Log("Test");
    }
}