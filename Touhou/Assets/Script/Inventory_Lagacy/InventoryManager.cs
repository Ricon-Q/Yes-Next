using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance
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
        else { Destroy(this.gameObject); }
    }

//===============================================================//

    private void Start()
    {
        SetupInventory();   
    }

    
    public void IsActive()
    {
        Debug.Log("Inventory Manager Is " + gameObject.activeSelf);
    }

    private void Update()
    {
        // Tab : 인벤토리 토글
        // if (InputManager.Instance.GetToggleInventoryPressed() )
        // {
        //     Debug.Log("ToogleInv");
            
        //     if(ShopManager.Instance.isShopMode) return;
            
        //     ToggleInventory();
        // }
    }

    [Header("Inventory")]
    public GameObject InventoryCanvas;
    public bool isInventoryOpen = false;
    public DynamicInventoryDisplay invToDisplay;
    public InventorySystem playerInventory;

    [Header("Info")]
    public Image characterPortrait;
    public TextMeshProUGUI characterName;
    [Header("Portrait Sprite")]
    public Sprite maleSprite;
    public Sprite femaleSprite;

    public void UpdateCharacterInfo()
    {
        if(_PlayerManager.Instance.playerData.isMale)
            characterPortrait.sprite = maleSprite;
        else
            characterPortrait.sprite = femaleSprite;
        characterName.text =_PlayerManager.Instance.playerData.name; 
        // characterPortrait.sprite = _PlayerManager.Instance.playerData.playerPortrait;
    }

    private void SetupInventory()
    {
        InventoryCanvas.SetActive(false);
    }

    //

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        InventoryCanvas.SetActive(isInventoryOpen);
        invToDisplay.isInfoOpen = false;
        invToDisplay.infoPanel.SetActive(false);
    }
}