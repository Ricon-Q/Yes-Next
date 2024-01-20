using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAndPestle : MonoBehaviour
{
    public void EnterCraftMode()
    {
    }

    public void ExitToolMode()
    {
        _CraftManager.Instance.object_MortarAndPestle.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
