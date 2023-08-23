using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public string targetSceneName; // 변수로 씬 이름을 저장합니다.
    // public Vector3 playerTargetPosition;

    public Vector3 DoorWayPosition;

    public void Interact(GameObject scanObj)
    {
        // Debug.Log("Interacting with the door");
        
        if (!string.IsNullOrEmpty(targetSceneName))
        {   
            // FindObjectOfType<PlayerManager>().setPlayerPosition(DoorWayPosition);
            PlayerManager.Instance.setPlayerPosition(DoorWayPosition);
            // FindObjectOfType<PlayerManager>().SavePlayerPosition();
            // FindObjectOfType<PlayerManager>().increaseMinute(5);
            TimeManager.Instance.increaseMinute(5);
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("No target scene name specified for the door.");
        }
    }
}
