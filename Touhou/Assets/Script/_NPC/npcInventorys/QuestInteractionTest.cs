using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractionTest : InteractionNonDialogue
{
    override public void Interaction()
    {
        QuestManager.Instance.EnterGuildQuestList();
    }
}
