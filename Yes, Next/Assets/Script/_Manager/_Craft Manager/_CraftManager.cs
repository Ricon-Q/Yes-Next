using TMPro;
using UnityEngine;

public class _CraftManager : MonoBehaviour
{
    private static _CraftManager instance;

    public static _CraftManager Instance
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

    // ============================================ //

    [Header("Potion Making Tools")]
    public GameObject craftModeCanvas;
    public GameObject craftToolCanvas;

    private void Start()
    {
        ExitCraftMode();
    }

    public void EnterCraftMode()
    {
        craftModeCanvas.SetActive(true);
    }

    public void ExitCraftMode()
    {
        craftModeCanvas.SetActive(false);
        craftToolCanvas.SetActive(false);

        for (int i = 0; i < 6; i++)
        {
            ExitToolMode(i);
        }

        object_MortarAndPestle.SetActive(false);
        object_PotionPot.SetActive(false);
        object_PotionStand.SetActive(false);
        object_HerbPocket.SetActive(false);
        object_PotionSynthesizer.SetActive(false);
        object_PotionRecipebook.SetActive(false);

        warningPanel.SetActive(false);
    }

    [Header("Potion Making Tool Panel")]
    public MortarAndPestle mortarAndPestle;
    public PotionPot potionPot;
    public PotionStand potionStand;
    public HerbPocket herbPocket;
    public PotionSynthesizer potionSynthesizer;
    public PotionRecipebook potionRecipebook;

    [Header("Potion Making Tool GameObject")]
    public GameObject object_MortarAndPestle;
    public GameObject object_PotionPot;
    public GameObject object_PotionStand;
    public GameObject object_HerbPocket;
    public GameObject object_PotionSynthesizer;
    public GameObject object_PotionRecipebook;
    
    [Header("Warning Panel")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningText; 

    public void EnterToolMode(int toolIndex)
    {
        switch(toolIndex)
        {
            case 0:
                craftToolCanvas.SetActive(true);
                mortarAndPestle.EnterCraftMode();
                break;
            case 1:
                craftToolCanvas.SetActive(true);
                potionPot.EnterCraftMode();
                break;
            case 2:
                potionStand.EnterCraftMode();
                break;
            case 3:
                herbPocket.EnterCraftMode();
                break;
            case 4:
                craftToolCanvas.SetActive(true);
                potionSynthesizer.EnterCraftMode();
                break;
            case 5:
                potionRecipebook.EnterCraftMode();
                break;
            default:
                break;
        }
    }

    public void ExitToolMode(int toolIndex)
    {
        switch(toolIndex)
        {
            case 0:
                craftToolCanvas.SetActive(false);
                mortarAndPestle.ExitToolMode();
                break;
            case 1:
                craftToolCanvas.SetActive(false);
                potionPot.ExitToolMode();
                break;
            case 2:
                potionStand.ExitToolMode();
                break;
            case 3:
                herbPocket.ExitToolMode();
                break;
            case 4:
                craftToolCanvas.SetActive(false);
                potionSynthesizer.ExitToolMode();
                break;
            case 5:
                potionRecipebook.ExitToolMode();
                break;
            default:
                break;
        }
    }

    public void OnWarningPanel(string warningText)
    {
        warningPanel.SetActive(true);
        this.warningText.text = warningText.ToString(); 
    }

    public void OffWarningPanel()
    {
        warningPanel.SetActive(false);
    }
}
