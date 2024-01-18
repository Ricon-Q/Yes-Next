using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject craftModeCanvas;
    [SerializeField] private GameObject craftToolCanvas;

    private void Start()
    {
        craftModeCanvas.SetActive(false);
        craftToolCanvas.SetActive(false);

        object_MortarAndPestle.SetActive(false);
        object_PotionPot.SetActive(false);
        object_PotionStand.SetActive(false);
        object_HerbPocket.SetActive(false);
        object_PotionSynthesizer.SetActive(false);
        object_PotionRecipebook.SetActive(false);
    }

    public void EnterCraftMode()
    {
        craftModeCanvas.SetActive(true);
    }

    [Header("Potion Making Tool Panel")]
    [SerializeField] public MortarAndPestle mortarAndPestle;
    [SerializeField] public PotionPot potionPot;
    [SerializeField] public PotionStand potionStand;
    [SerializeField] public HerbPocket herbPocket;
    [SerializeField] public PotionSynthesizer potionSynthesizer;
    [SerializeField] public PotionRecipebook potionRecipebook;

    [Header("Potion Making Tool GameObject")]
    [SerializeField] public GameObject object_MortarAndPestle;
    [SerializeField] public GameObject object_PotionPot;
    [SerializeField] public GameObject object_PotionStand;
    [SerializeField] public GameObject object_HerbPocket;
    [SerializeField] public GameObject object_PotionSynthesizer;
    [SerializeField] public GameObject object_PotionRecipebook;
    

    public void EnterToolMode(int toolIndex)
    {
        craftToolCanvas.SetActive(true);
        switch(toolIndex)
        {
            case 0:
                mortarAndPestle.EnterCraftMode();
                break;
            case 1:
                potionPot.EnterCraftMode();
                break;
            case 2:
                potionStand.EnterCraftMode();
                break;
            case 3:
                herbPocket.EnterCraftMode();
                break;
            case 4:
                potionSynthesizer.EnterCraftMode();
                break;
            case 5:
                potionRecipebook.EnterCraftMode();
                break;
            default:
                break;
        }
    }
}
