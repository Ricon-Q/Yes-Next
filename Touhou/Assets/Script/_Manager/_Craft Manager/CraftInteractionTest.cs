using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInteractionTest : InteractionNonDialogue
{
    public override void Interaction()
    {
        _CraftManager.Instance.EnterCraftMode();
    }
}
