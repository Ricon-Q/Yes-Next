using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolIconInteract : MonoBehaviour
{
    public GameObject toolsGrid;
    
    public void ChangeTool(int index)
    {
        Vector3 newPosition = new Vector3(2000 - (index * 1500) ,0,0);
        Debug.Log(newPosition);
        toolsGrid.transform.localPosition = newPosition;
    }
}
