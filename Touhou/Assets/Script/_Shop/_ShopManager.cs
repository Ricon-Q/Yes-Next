using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class _ShopManager : MonoBehaviour
{
    private static _ShopManager instance;
    public static _ShopManager Instance
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

    // ============================================== //

    [Header("Shop UI")]
    public bool isShopMode;
    [SerializeField] private GameObject shopPanel;
    
    [Header("Display Panel")]
    [SerializeField] private _BuyDisplay buyDisplay;    // 중앙 상단 패널
    [SerializeField] private _SellDisplay sellDisplay;  // 중앙 하단 패널
    [SerializeField] private _ShopNpcDisplay shopNpcDisplay;    // 좌측 NPC 패널
    [SerializeField] private _ShopPlayerDisplay shopPlayerDisplay;  // 우측 Player 패널
    [SerializeField] private GameObject confirmPanel;

    [Header("NPC inventory")]
    [SerializeField] private npcInventoryHolder npcInventoryHolder;

    
    [Header("In Shop Mode")]
    private long totalPrice; // Player의 판매 금액 - 구매 금액
    private long  finalPrice; // TotalPrice와 Player의 소지금의 합
    [SerializeField] private TextMeshProUGUI totalPriceText; // totalPrice를 표시할 TMP
    [SerializeField] private GameObject warningPanel; // 소지금이 부족하여 거래가 이뤄질 수 없는 경우 표시되는 패널

    // [SerializeField] private GameObject affection5Panel;
    // [SerializeField] private GameObject affection10Panel;

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

    public void EnterShopMode(npcInventoryHolder npcInventoryHolder)
    {
        shopPanel.SetActive(true);
        this.npcInventoryHolder = npcInventoryHolder;
        isShopMode = true;

        shopNpcDisplay.EnterShopMode(this.npcInventoryHolder.inventorySystem);
        shopPlayerDisplay.EnterShopMode();
    }

    public void ExitShopMode()
    {
        shopPlayerDisplay.ExitShopMode();
        buyDisplay.ExitShopMode();
        sellDisplay.ExitShopMode();

        shopPanel.SetActive(false);
        isShopMode = false;
    }


    public void calculateTotal()
    {
        totalPrice = shopPlayerDisplay.totalSellPrice - shopNpcDisplay.totalBuyPrice;
        if(totalPrice > 0) { totalPriceText.text = "+" + totalPrice.ToString("n0"); }
        else { totalPriceText.text = totalPrice.ToString("n0"); }
    }

    public void ResetShop()
    {
        // shopPlayerDisplay.Reset();
        shopNpcDisplay.Reset();
        sellDisplay.Reset();
        buyDisplay.Reset();
        
        shopPlayerDisplay.Reset();
        Debug.Log("Resetshop");
    }

    public void ConfirmDeal()
    {
        finalPrice = totalPrice + _PlayerManager.Instance.playerData.money;
        if(finalPrice >= 0)
        {
            _PlayerManager.Instance.playerData.money = finalPrice;
            
            shopNpcDisplay.ConfirmDeal();
            sellDisplay.ConfirmDeal();
            buyDisplay.ConfirmDeal();
            shopPlayerDisplay.ConfirmDeal();

            Debug.Log("Success Deal");
        }
        else
        {   
            warningPanel.SetActive(true);
            Debug.Log("Fail Deal");
        }
    }
}
