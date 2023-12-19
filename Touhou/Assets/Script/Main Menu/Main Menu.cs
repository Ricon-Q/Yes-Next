using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlaySave(int slotIndex)
    {

    }

    public void Button_Option()
    {

    }

    public void Button_Quit()
    {
        Application.Quit();
    }

    [Header("GameSlots")]
    // public GameObject[] slots;
    string path = "";
    public SaveLoadUI[] saveLoadUIs;

    public GameObject deleteSavePanel;
    public TextMeshProUGUI deleteSaveText;
    public int saveIndex = -1;

    private void Start()
    {
        CheckSlots();
        deleteSavePanel.SetActive(false);
    }

    public void CheckSlots()
    {
        for (int i = 0; i < saveLoadUIs.Length; i++)
        {
            path = "Saves/SaveSlot" + i + ".es3";
            if(ES3.FileExists(path))
            {
                saveLoadUIs[i].EnableContinue();
            }
            else
                saveLoadUIs[i].DisableContinue();
        }
    }

    public void OpenDeleteSavePanel(int saveIndex)
    {
        deleteSavePanel.SetActive(true);
        deleteSaveText.text = "Delete save #" + (saveIndex+1) +"?\n"
                            + "This can't be UNDO!";
        this.saveIndex = saveIndex;
    }

    public void DeleteSave()
    {
        DataManager.Instance.DeleteSave(saveIndex);
        saveLoadUIs[saveIndex].DisableContinue();
    }
    public void CancleDeleteSave()
    {
        saveIndex = -1;
    }
}
