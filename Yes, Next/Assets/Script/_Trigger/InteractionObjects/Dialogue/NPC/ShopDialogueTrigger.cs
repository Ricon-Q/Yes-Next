using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class ShopDialogueTrigger : npcDialogueTrigger
{
    [Header("Shop Inventory Holder")]
    [SerializeField] private npcInventoryHolder _npcInventoryHolder;
    override public void EndConversation()
    {
        bool _isHospitalTrigger = DialogueLua.GetVariable("shopTrigger").asBool;
        // Debug.Log(_isHospitalTrigger);
        if(!_isHospitalTrigger)
            PlayerInputManager.SetPlayerInput(true) ;
        else
            _ShopManager.Instance.EnterShopMode(_npcInventoryHolder);
    }

}
