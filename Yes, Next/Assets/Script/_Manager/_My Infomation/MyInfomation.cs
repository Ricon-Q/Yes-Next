using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInfomation : MonoBehaviour
{
    private static MyInfomation instance;

    public static MyInfomation Instance
    {
        get
        {
            if(instance == null)
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

    // ========================================================== //

    [Header("Panel Object")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _guideBookPanel;
    [SerializeField] private GameObject _hospitalInfoPanel;
    [SerializeField] private GameObject _optionPanel;

    [Header("Button Grid")]
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _guideBookButton;
    [SerializeField] private Button _hospitalInfoButton;
    [SerializeField] private Button _optionButton;
    public bool isMyInfoOpen;

    [Header("Button Text Frame")]
    [SerializeField] private GameObject _inventoryTextFrame;
    [SerializeField] private GameObject _guideTextFrame;
    [SerializeField] private GameObject _hospitalTextFrame;
    [SerializeField] private GameObject _optionTextFrame;

    private void Start() 
    {
        OffMainPanel();
    }

    public void OffMainPanel()
    {
        UiManager.Instance.ToggleUiCanvas(true);
        OffAllPanel();
        _mainPanel.SetActive(false);
        isMyInfoOpen = false;
    }
    public void OffAllPanel()
    {
        _inventoryPanel.SetActive(false);
        _guideBookPanel.SetActive(false);
        _hospitalInfoPanel.SetActive(false);
        _optionPanel.SetActive(false);
    }
    public void InteractableAllButton()
    {
        _inventoryButton.interactable = true;
        _guideBookButton.interactable = true;
        _hospitalInfoButton.interactable = true;
        _optionButton.interactable = true;
    }

    // 탭 토글
    public void ToggleInventory()
    {
        InteractableAllButton();
        _inventoryButton.interactable = false;

        _mainPanel.SetActive(true);
        OffAllPanel();  
        _inventoryPanel.SetActive(true);

        isMyInfoOpen = true;

        UiManager.Instance.inventoryHotBarDisplay.RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
        
        UiManager.Instance.ToggleUiCanvas(false);
    }
    public void ToggleGuideBook()
    {
        InteractableAllButton();
        _guideBookButton.interactable = false;

        _mainPanel.SetActive(true);
        OffAllPanel();  
        _guideBookPanel.SetActive(true);

        isMyInfoOpen = true;

        UiManager.Instance.ToggleUiCanvas(false);
    }

    public void ToggleHospitalInfo()
    {
        InteractableAllButton();
        _hospitalInfoButton.interactable = false;

        _mainPanel.SetActive(true);
        OffAllPanel();  
        _hospitalInfoPanel.SetActive(true);

        isMyInfoOpen = true;

        UiManager.Instance.ToggleUiCanvas(false);
    }

    public void ToggleOption()
    {
        InteractableAllButton();
        _optionButton.interactable = false;

        _mainPanel.SetActive(true);
        OffAllPanel();  
        _optionPanel.SetActive(true);

        isMyInfoOpen = true;
        
        UiManager.Instance.ToggleUiCanvas(false);
    }

    public void MyInfoEscape()
    {
        if(isMyInfoOpen) OffMainPanel();
        else if(!isMyInfoOpen)  ToggleOption();
    }

    // 버튼에 마우스 올려둘시 텍스트 출력
    public void OnInventoryTextFrame()
    {
        _inventoryTextFrame.SetActive(true);
    }
    public void OffInventoryTextFrame()
    {
        _inventoryTextFrame.SetActive(false);
    }

    public void OnGuideTextFrame()
    {
        _guideTextFrame.SetActive(true);
    }
    public void OffGuideTextFrame()
    {
        _guideTextFrame.SetActive(false);
    }

    public void OnHospitalTextFrame()
    {
        _hospitalTextFrame.SetActive(true);
    }
    public void OffHospitalTextFrame()
    {
        _hospitalTextFrame.SetActive(false);
    }

    public void OnOptionTextFrame()
    {
        _optionTextFrame.SetActive(true);
    }
    public void OffOptionTextFrame()
    {
        _optionTextFrame.SetActive(false);
    }
}
