using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionStand : MonoBehaviour
{
    public void EnterCraftMode()
    {
        
    }
    public void ExitToolMode()
    {
        _CraftManager.Instance.object_PotionStand.SetActive(false);
    }
}
