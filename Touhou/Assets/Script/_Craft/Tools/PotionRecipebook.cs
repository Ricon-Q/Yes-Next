using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRecipebook : MonoBehaviour
{
    public void EnterCraftMode() 
    {
        
    }
    public void ExitToolMode()
    {
        _CraftManager.Instance.object_PotionRecipebook.SetActive(false);
    }
}
