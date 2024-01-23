using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    public static UiManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // ======================================================= //

    [Header("UI Canvas")]
    public GameObject uiCanvas;
    private bool isUiCanvasOn = true;

    [Header("Time Frame")]
    [SerializeField] private TextMeshProUGUI yearMonthDayText;
    [SerializeField] private TextMeshProUGUI timeText;
    private string season = "";

    private void Update() 
    {
        MonthToSeason();
        // Year, Season, and Day
        string formattedDay = _TimeManager.Instance.timeData.day < 10 ? "0" + _TimeManager.Instance.timeData.day.ToString() : _TimeManager.Instance.timeData.day.ToString();
        yearMonthDayText.text = $"{_TimeManager.Instance.timeData.year}년차 {season} {formattedDay}일";

        // Hour and Minute
        string formattedHour = _TimeManager.Instance.timeData.hour < 10 ? "0" + _TimeManager.Instance.timeData.hour.ToString() : _TimeManager.Instance.timeData.hour.ToString();
        string formattedMinute = _TimeManager.Instance.timeData.minute < 10 ? "0" + _TimeManager.Instance.timeData.minute.ToString() : _TimeManager.Instance.timeData.minute.ToString();
        timeText.text = $"{formattedHour}:{formattedMinute}";
    }   

    public void MonthToSeason()
    {
        switch (_TimeManager.Instance.timeData.month)
        {
            case 1:
                season = "봄 ";
                break;
            case 2:
                season = "여름 ";
                break;
            case 3:
                season = "가을 ";
                break;
            case 4:
                season = "겨울 ";
                break;
            default:
                break;
        }
    }

    public void ToggleUiCanvas()
    {
        if(isUiCanvasOn)
        {
            uiCanvas.SetActive(false);
            isUiCanvasOn = false;
        }
        else
        {
            uiCanvas.SetActive(true);
            isUiCanvasOn = true;
        }

    }

    [Header("Time Frame")]
    [SerializeField] private DOTweenAnimation dOTweenAnimation;
    public GameObject buttonIn;
    public GameObject buttonOut;

    public void TimeFrameIn()
    {
        buttonIn.SetActive(false);
        buttonOut.SetActive(true);
        dOTweenAnimation.DOPlayForward();
    }
    
    public void TimeFrameOut()
    {
        buttonIn.SetActive(true);
        buttonOut.SetActive(false);
        dOTweenAnimation.DOPlayBackwards();
    }
}
