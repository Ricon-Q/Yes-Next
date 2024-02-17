using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class HospitalManager : MonoBehaviour
{
    private static HospitalManager instance;

    public static HospitalManager Instance
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
            return;
        }
    }

    // ===================================================== //
    [Header("UI Canvas")]
    [SerializeField] private GameObject _hospitalCanvas;

    [Header("Hospital Script")]
    public InfoUiDisplay _infoUiDisplay;
    public DialoguePanel _dialoguePanel;
    public DiagnosisPanel _diagnosisPanel;
    public Hospital_InventoryDisplay _hospital_InventoryDisplay;
    public HospitalGuideBook _hospitalGuideBook;
    
    private void Start()
    {
        ExitHospitalMode();
    }

    public void EnterHospitalMode()
    {
        _hospitalCanvas.SetActive(true);
        
        _infoUiDisplay.EnterHospitalMode();
        _dialoguePanel.EnterHospitalMode();

        MyInfomation.Instance.ExitMyInfomation();
        PlayerInputManager.SetPlayerInput(false);
        _hospital_InventoryDisplay.ChangeInventory(0);
        _hospitalGuideBook.EnterHospitalMode();

        _diagnosisPanel._diagnosisData = new DiagnosisData();
    }

    public void ExitHospitalMode()
    {
        _infoUiDisplay.ExitHospitalMode();
        _dialoguePanel.ExitHospitalMode();

        _hospitalCanvas.SetActive(false);
        _hospitalGuideBook.CloseGuideBook();        
        PlayerInputManager.SetPlayerInput(true);
    }
}
