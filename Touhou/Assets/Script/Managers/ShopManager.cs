using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// 상점을 담당하는 싱글톤 스크립트
// V1. DialogueManager에서 상점 선택지를 누르면 EnterShopMode 함수가 실행되며 ShopManager 스크립트로 넘어간다. 
// V2. 상점 스크립트에선 판매자(NPC)의 인벤토리, 호감도, 이름, 초상화와 같은 정보들을 할당한다.
// 3. 해당 정보들을 바탕으로 왼쪽에 NPC 패널을 채운다.
// 4. 오른쪽에는 플레이어의 인벤토리를 받아온다. 인벤토리에서 상점에 판매할 수 없는 아이템은 제외한다.
// 5. 가운데는 거래창이 있으며 상단에는 판매 모드, 구매 모드 버튼이 있다. 
// 6. 판매 모드일때는 플레이어 인벤토리만 클릭할 수 있으며 구매 모드에서는 NPC의 판매 목록만 클릭할 수 있다.
// 7. 어느 모드에서 해당하는 인벤토리의 원하는 아이템을 클릭하면 해당 아이템은 강조표시가 되며, 같은 아이템이 가운데 거래창에 생긴다.
// 8. 원하는 아이템을 거래창에 담고, 하단의 거래 진행 버튼을 클릭하면 거래가 진행된다.
// 9. 판매, 구매모드 모두에서 NPC의 인벤토리는 변하지 않는다.
// 10. 판매 모드에서 플레이어가 판매한 아이템은 플레이어의 인벤토리에서 삭제된다.
// 11. 구매 모드에서 플레이어가 구매한 아이템은 플레이어의 인벤토리에 추가된다.
// 12. 초기화 버튼을 누르면 거래창이 비워지며 선택된 아이템의 강조표시가 사라진다.
public class ShopManager : MonoBehaviour
{
    public bool isShopMode;

    [Header("Shop UI")]
    [SerializeField] private GameObject shopPanel;

    [Header("NPC Panel")]
    // [SerializeField] private GameObject npcPanel;
    // [SerializeField] private GameObject npcPotrait;
    // [SerializeField] private GameObject textFrame;
    // [SerializeField] private GameObject affectionFram;
    // [SerializeField] private GameObject itemFrame;


    [Header("Player Panel")]
    // [SerializeField] private GameObject playerPanel;
    // [SerializeField] private GameObject displayInventory;
    [SerializeField] private ShopPlayerDisplay playerDisplay;

    [Header("Middle Panel")]
    // [SerializeField] private GameObject middlePanel;
    public BuyDisplay buyDisplay;
    public SellDisplay sellDisplay;
    public ShopNpcDisplay shopNpcDisplay;
    public ShopPlayerDisplay shopPlayerDisplay;

    [Header("NPC Info")]
    [SerializeField] private NPC npcInfo;

    [Header("Player Info")]
    // [SerializeField] private InventoryObject playerInventory;
    // public GameObject inventoryPrefab;

    public GameObject confirmPanel;

    private long total;
    private long finalPrice;
    public TextMeshProUGUI totalText;
    public GameObject warningPanel;
    public GameObject affection5Panel;
    public GameObject affection10Panel;

    private static ShopManager instance;
    public static ShopManager Instance
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
            // DontDestroyOnLoad(this.gameObject);
        }    
        else { Destroy(this.gameObject); }
    }

    private void Start() 
    {
        confirmPanel.SetActive(false);
        shopPanel.SetActive(false);
        warningPanel.SetActive(false);
        isShopMode = false;
    }

    private void Update()
    {
        calculateTotal();
    }

    public void EnterShopMode(NPC npcScript)
    {
        shopPanel.SetActive(true);
        npcInfo = npcScript;
        isShopMode = true;
        if(npcInfo.npcData.affection < 5)
        {
            affection5Panel.SetActive(true);
            affection10Panel.SetActive(true);
        }
        else if(5 <= npcInfo.npcData.affection && npcInfo.npcData.affection < 10)
        {
            affection5Panel.SetActive(false);
            affection10Panel.SetActive(true);
        }
        else if(10 <= npcInfo.npcData.affection)
        {
            affection5Panel.SetActive(false);
            affection10Panel.SetActive(false);
        }

        // playerDisplay.EnterShopMode();
        shopNpcDisplay.EnterShopMode(npcInfo.inventoryObject);
        shopPlayerDisplay.EnterShopMode();
        buyDisplay.EnterShopMode();
        sellDisplay.EnterShopMode();
        
    }

    public void ExitShopMode()
    {
        shopNpcDisplay.ExitShopMode();
        shopPlayerDisplay.ExitShopMode();
        buyDisplay.ExitShopMode();
        sellDisplay.ExitShopMode();

        shopPanel.SetActive(false);
        isShopMode = false;
    }

    public void calculateTotal()
    {
        total = shopPlayerDisplay.totalSellPrice - shopNpcDisplay.totalBuyPrice;
        totalText.text = total.ToString("n0");
    }

    public void ResetShop()
    {
        shopPlayerDisplay.Reset();
        shopNpcDisplay.Reset();
        sellDisplay.Reset();
        buyDisplay.Reset();
        Debug.Log("Resetshop");
    }

    public void ConfirmDeal()
    {
        finalPrice = total + PlayerManager.Instance.playerData.money;
        if(finalPrice >= 0)
        {
            PlayerManager.Instance.playerData.money = finalPrice;
            shopPlayerDisplay.ConfirmDeal();
            shopNpcDisplay.ConfirmDeal();
            sellDisplay.ConfirmDeal();
            buyDisplay.ConfirmDeal();

            Debug.Log("Success Deal");
        }
        else
        {   
            warningPanel.SetActive(true);
            Debug.Log("Fail Deal");
        }
    }
}
