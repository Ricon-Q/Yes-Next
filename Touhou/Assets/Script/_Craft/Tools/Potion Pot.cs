using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPot : MonoBehaviour
{
    public void EnterCraftMode()
    {
        
    }
    public void ExitToolMode()
    {
        _CraftManager.Instance.object_PotionPot.SetActive(false);
    }
}
