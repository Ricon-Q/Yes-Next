using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            DontDestroyOnLoad(this.gameObject);
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
        yearMonthDayText.text = _TimeManager.Instance.timeData.year + "년차 " + season + _TimeManager.Instance.timeData.day + "일";
        timeText.text = _TimeManager.Instance.timeData.hour + ":" + _TimeManager.Instance.timeData.minute;
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
}
