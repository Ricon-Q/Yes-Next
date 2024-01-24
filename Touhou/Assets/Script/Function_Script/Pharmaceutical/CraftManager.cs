// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CraftManager : MonoBehaviour
// {
//     public string ExitSceneName;
//     public Vector3 ExitPlayerPosition;
//     public GameObject toolsGrid;

//     public List<MedicineCraftSystem> tools;
    
//     public void ChangeTool(int index)
//     {
//         foreach (var tool in tools)
//         {
//             if(tool.IsItemDataNull() == false) return;
//         }

//         toolsGrid.transform.localPosition = new Vector3(2000 - (index * 1500) ,0,0);;

//         // Vector3 newPosition = new Vector3(2000 - (index * 1500) ,0,0);
//         // Debug.Log(newPosition);
//         // toolsGrid.transform.localPosition = new Vector3(2000 - (index * 1500) ,0,0);;
//     }

//     public void ExitCraftMode()
//     {
//         foreach (var tool in tools)
//         {
//             if(tool.IsItemDataNull() == false) return;
//         }
//         PlayerManager.Instance.SetPlayerPosition(ExitPlayerPosition);
//         PlayerManager.Instance.AddCurrentFatigue(-1);
//         PlayerManager.Instance.AddCurrentHunger(-1);
//         TimeManager.Instance.increaseMinute(5);
//         UnityEngine.SceneManagement.SceneManager.LoadScene(ExitSceneName);
//     }
// }
