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

    [Header("Dialogue Database")]
    public DialogueDatabase _mainDialogueDatabase;
    public DialogueDatabase _hospitalDialogueDatabase;
    private void Start()
    {
        ExitHospitalMode();
    }

    public void EnterHospitalMode()
    {
        _hospitalCanvas.SetActive(true);
        _infoUiDisplay.EnterHospitalMode();
        
    }

    public void ExitHospitalMode()
    {
        _infoUiDisplay.ExitHospitalMode();
        _hospitalCanvas.SetActive(false);
    }
}
