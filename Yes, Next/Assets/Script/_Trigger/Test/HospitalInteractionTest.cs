using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalInteractionTest : InteractionNonDialogue
{
    public override void Interaction()
    {
        HospitalManager.Instance.EnterHospitalMode();
    }
}
