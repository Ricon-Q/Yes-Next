using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenuInteractionTest : InteractionNonDialogue
{
    public override void Interaction()
    {
        QuestManager.Instance.EnterQuestMenu();
    }
}
