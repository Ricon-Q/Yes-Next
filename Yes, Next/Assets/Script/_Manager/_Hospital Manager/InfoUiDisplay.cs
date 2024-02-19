using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfoUiDisplay : MonoBehaviour
{
    [Header("Time Display")]
    [SerializeField] private TextMeshProUGUI _timeDisplayText;

    [Header("Button Grid")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _endHospitalMode;

    [Header("Item Description")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription; 
    
    private string season;

    public void EnterHospitalMode()
    {
        RefreshTimeDisplay();
    }

    public void ExitHospitalMode()
    {

    }

    public void RefreshTimeDisplay()
    {
        MonthToSeason();

        int _hour = _TimeManager.Instance.timeData.hour;
        int _minute = _TimeManager.Instance.timeData.minute;

        string formattedDay = _TimeManager.Instance.timeData.day < 10 ? "0" + _TimeManager.Instance.timeData.day.ToString() : _TimeManager.Instance.timeData.day.ToString();
        _timeDisplayText.text = $"{_TimeManager.Instance.timeData.year}년차 {season} {formattedDay}일\n";
        if(_hour > 12)
            _timeDisplayText.text += string.Format("오후 {0}시 {1}분", _hour-12, _minute);
        else if(_hour == 12)
            _timeDisplayText.text += string.Format("오후 {0}시 {1}분", _hour, _minute);
        else
            _timeDisplayText.text += string.Format("오전 {0}시 {1}분", _hour, _minute);
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

    public void AllocateItemData(InventoryItemData inventoryItemData)
    {
        _itemImage.color = Color.white;
        _itemImage.sprite = inventoryItemData.Icon;
        _itemName.text = inventoryItemData.DisplayName;
        _itemDescription.text = inventoryItemData.Description;
    }  

    public void DisallocateItemData()
    {
        _itemImage.color = Color.clear;
        _itemImage.sprite = null;
        _itemName.text = "";
        _itemDescription.text = "아이템을 선택하여 정보 확인";
    }

    public void BasicPotion(Sprite sprite)
    {
        _itemImage.color = Color.white;
        _itemImage.sprite = sprite;
        _itemName.text = "회복약";
        _itemDescription.text = "병원 창고에 한가득 쌓여있는 회복약.\n손쉽게 만들 수 있지만 그만큼 효과는 약하다";
    }
}