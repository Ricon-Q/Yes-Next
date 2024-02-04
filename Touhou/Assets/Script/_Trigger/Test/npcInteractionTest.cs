using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteractionTest : InteractionNonDialogue
{
    public npcInventoryHolder npcInventoryHolder;
    override public void Interaction()
    {
        _ShopManager.Instance.EnterShopMode(npcInventoryHolder);
    }
}
