using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    
    public GameObject newGame;
    public GameObject loadPanel;
    public GameObject emptySavePanel;

    [Header("Info Panel")]
    [SerializeField] private TextMeshProUGUI firstName;
    [SerializeField] private TextMeshProUGUI lastName;
    [SerializeField] private TextMeshProUGUI timeYearMonthDay;
    [SerializeField] private TextMeshProUGUI timeHourMinute;
    [SerializeField] private TextMeshProUGUI hospitalLevel;
    [SerializeField] private TextMeshProUGUI money;
    private string season = "";

    public void EnableContinue()
    {
        newGame.SetActive(false);
        loadPanel.SetActive(true);
        emptySavePanel.SetActive(false);
    }

    public void DisableContinue()
    {
        newGame.SetActive(true);
        loadPanel.SetActive(false);
        emptySavePanel.SetActive(true);
    }

    public void InfoPanelOn(int saveIndex)
    {
        
        PlayerSaveData loadInfo;
        loadInfo = DataManager.Instance.LoadInfo(saveIndex);

        MonthToSeason(loadInfo);

        firstName.text = loadInfo.playerData.firstName;
        lastName.text = loadInfo.playerData.lastName;

        string formattedDay = loadInfo.timeData.day < 10 ? "0" + loadInfo.timeData.day.ToString() : loadInfo.timeData.day.ToString();
        timeYearMonthDay.text = $"{loadInfo.timeData.year}년차 {season} {formattedDay}일";

        string formattedHour = loadInfo.timeData.hour < 10 ? "0" + loadInfo.timeData.hour.ToString() : loadInfo.timeData.hour.ToString();
        string formattedMinute = loadInfo.timeData.minute < 10 ? "0" + loadInfo.timeData.minute.ToString() : loadInfo.timeData.minute.ToString();
        timeHourMinute.text = $"{formattedHour}시 {formattedMinute}분";

        hospitalLevel.text = $"Lv {loadInfo.playerData.hospitalLevel}";
        money.text = loadInfo.playerData.money.ToString();
    }
    public void MonthToSeason(PlayerSaveData loadInfo)
    {
        switch (loadInfo.timeData.month)
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

}
