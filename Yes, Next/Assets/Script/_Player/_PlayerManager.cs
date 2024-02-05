using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _PlayerManager : MonoBehaviour
{
    private static _PlayerManager instance;

    public static _PlayerManager Instance
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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    //====================================================//
    [Header("Player Object")]
    [SerializeField] private GameObject playerObject; 
    // [SerializeField] private AreaData defaultArea;
    
    public PlayerData playerData;
    private void Start() 
    {
        playerData = new PlayerData();
    }
    
    public void IsActive()
    {
        Debug.Log("Player Manager Is " + gameObject.activeSelf);
    }

    public void TogglePlayer(bool active)
    {
        playerObject.SetActive(active);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DataManager.Instance.SaveSlot();
            Debug.Log("Saved");
        }   
        if(Input.GetKeyDown(KeyCode.P))
        {
            DataManager.Instance.LoadInventory(DataManager.Instance.currentSaveIndex);
            Debug.Log("Saved");
        }  
    }
}

public class PlayerData
{
    public string currentArea;
    public string firstName;
    public string lastName;
    // public Sprite playerPortrait;
    public bool isMale;
    public int hospitalLevel;    
    public long money;
    public float maxHealth;        // 최대 체력
    public float currentHealth;    // 현재 체력
    public float maxStamina;       // 최대 피로도
    public float currentStamina;   // 현재 피로도

    public PlayerData()
    {
        currentArea = "defaultArea";
        firstName = "Rin";
        lastName = "";
        isMale = true;
        // playerPortrait = null;
        hospitalLevel = 0;
        money = 50000;

        maxHealth = 100;
        currentHealth = 100;    
        maxStamina = 100;       
        currentStamina = 100;
    }

    // // =========SetFunc=========
    // public void SetMaxHealth(float value)
    // {
    //     maxHealth = value;
    // }
    // public void SetCurrentHealth(float value)
    // {
    //     currentHealth = value;
    // }
    // public void SetMaxStamina(float value)
    // {
    //     maxStamina = value;
    // }
    // public void SetCurrentStamina(float value)
    // {
    //     currentStamina = value;
    // }
    // public void SetHospitalLevel(int value)
    // {
    //     hospitalLevel = value;
    // }

    // =========AddFunc=========
    public void AddHealth(float value)
    {
        maxHealth += value;
    }
    public void AddCurrentHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, -1, maxHealth);
    }
    public void AddMaxStamina(float value)
    {
        maxStamina += value;
    }
    public void AddCurrentStamina(float value)
    {
        currentStamina = Mathf.Clamp(currentStamina + value, -1, maxStamina);
        if( currentStamina < 0 && 
            ( 0<= _TimeManager.Instance.timeData.hour && _TimeManager.Instance.timeData.hour < 6) && 
            SceneManager.GetActiveScene().name != "Home")
        {
            EventManager.Instance._eventDictionary[0].TriggerOn();
        }
    }
    public void AddMoney(long value)
    {
        money += value;
    }
    public void AddHospitalLevel(int value)
    {
        hospitalLevel += value;
    }

    // // =========GetFunc=========
    // public float GetMaxHealth()
    // {
    //     return maxHealth;
    // }
    // public float GetCurrentHealth()
    // {
    //     return currentHealth;
    // }
    // public float GetMaxStamina()
    // {
    //     return maxStamina;
    // }
    // public float GetCurrentStamina()
    // {
    //     return currentStamina;
    // }
    // public float GetMoney()
    // {
    //     return money;
    // }
    // public float GetHospitallevel()
    // {
    //     return hospitalLevel;
    // }
}
