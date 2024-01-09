using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}

public class PlayerData
{
    public string name;
    // public Sprite playerPortrait;
    public bool isMale;
    public int hospitalLevel;    
    public long money;

    public float maxHealth;        // 최대 체력
    public float currentHealth;    // 현재 체력
    public float maxFatigue;       // 최대 피로도
    public float currentFatigue;   // 현재 피로도

    public PlayerData()
    {
        name = "Rin";
        isMale = true;
        // playerPortrait = null;
        hospitalLevel = 0;
        money = 50000;

        maxHealth = 100;
        currentHealth = 100;    
        maxFatigue = 100;       
        currentFatigue = 100;
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
    // public void SetMaxFatigue(float value)
    // {
    //     maxFatigue = value;
    // }
    // public void SetCurrentFatigue(float value)
    // {
    //     currentFatigue = value;
    // }
    // public void SetHospitalLevel(int value)
    // {
    //     hospitalLevel = value;
    // }

    // // =========AddFunc=========
    // public void AddMaxHealth(float value)
    // {
    //     maxHealth += value;
    // }
    // public void AddCurrentHealth(float value)
    // {
    //     currentHealth += value;
    // }
    // public void AddMaxFatigue(float value)
    // {
    //     maxFatigue += value;
    // }
    // public void AddCurrentFatigue(float value)
    // {
    //     currentFatigue += value;
    // }
    // public void AddMoney(long value)
    // {
    //     money += value;
    // }
    // public void AddHospitalLevel(int value)
    // {
    //     hospitalLevel += value;
    // }

    // // =========GetFunc=========
    // public float GetMaxHealth()
    // {
    //     return maxHealth;
    // }
    // public float GetCurrentHealth()
    // {
    //     return currentHealth;
    // }
    // public float GetMaxFatigue()
    // {
    //     return maxFatigue;
    // }
    // public float GetCurrentFatigue()
    // {
    //     return currentFatigue;
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
