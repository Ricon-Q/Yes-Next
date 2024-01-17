using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CraftManager : MonoBehaviour
{
    private static _CraftManager instance;

    public static _CraftManager Instance
    {
        get
        {
            if (instance == null)
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
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // ============================================ //

    public void EnterCraftMode()
    {
        
    }
}
