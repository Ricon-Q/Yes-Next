using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//키보드 방향키를 이용한 상하좌우 이동

public class PlayerManager : MonoBehaviour, IDataPersistence
{

    public float speed;     // 이동 속도
    public PlayerData playerData;

    private static PlayerManager instance;

    public static PlayerManager Instance
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

    Rigidbody2D rigid;
    Vector3 dirVec;
    GameObject ScanObject;

    private Vector3 playerPosition;
    // public InventoryObject inventory;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

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

    private void Update()
    {
        // //Inventory Save Load
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // inventory.Save();
            Debug.Log("Saved");
        }    
        if(Input.GetKeyDown(KeyCode.A))
        {
            // inventory.Load();
            
            Debug.Log("Loaded");
        }

    }

    private void HandleMove()
    {
        dirVec = InputManager.Instance.GetMoveDirection();
        rigid.velocity = dirVec * speed;
        // Debug.Log(rigid.velocity);
    }

    private void FixedUpdate()
    {   
        if(DialogueManager.Instance.dialogueIsPlaying )
            // || InventoryManager.Instance.isInventoryOpen 
            // || ShopManager.Instance.isShopMode)
        {
            return;
        }
        HandleMove();
        DrawScanRay();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = playerPosition;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void DrawScanRay()
    {
        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Interactable"));

        if(rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
        }
        else
        {
            ScanObject = null;
        }
    }
    
    public void SetPlayerPosition(Vector3 DoorWayPosition)
    {
        playerPosition = DoorWayPosition;
    }
    
    public void LoadData(GameData data)
    {
        // Debug.Log("Player Data Load");
        this.playerData = data.playerData;
    }
    
    public void SaveData(ref GameData data)
    {
        // Debug.Log("Player Data Save");
        data.playerData = this.playerData;
    }
    
    public void ChangeCurrentHealth(float value)
    {
        this.playerData.currentHealth = value;
    }
    public void ChangeMaxHealth(float value)
    {
        this.playerData.maxHealth = value;
    }
    public void ChangeCurrentFatigue(float value)
    {
        this.playerData.currentFatigue = value;
    }
    public void ChangeMaxFatigue(float value)
    {
        this.playerData.maxFatigue = value;
    }
    public void ChangeCurrentHunger(float value)
    {
        this.playerData.currentHunger = value;
    }
    public void ChangeMaxHunger(float value)
    {
        this.playerData.maxHunger = value;
    } 
    public void AddCurrentHealth(float value)
    {
        this.playerData.currentHealth += value;
    }
    public void AddMaxHealth(float value)
    {
        this.playerData.maxHealth += value;
    }
    public void AddCurrentFatigue(float value)
    {
        this.playerData.currentFatigue += value;
    }
    public void AddMaxFatigue(float value)
    {
        this.playerData.maxFatigue += value;
    }
    public void AddCurrentHunger(float value)
    {
        this.playerData.currentHunger += value;
    }
    public void AddMaxHunger(float value)
    {
        this.playerData.maxHunger += value;
    }

}