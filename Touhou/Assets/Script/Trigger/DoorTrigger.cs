using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*/ 
    Door Trigger
    Change Scene
/*/
public class DoorTrigger : MonoBehaviour
{
    public string targetSceneName; // 변수로 씬 이름을 저장합니다.
    // public Vector3 playerTargetPosition;

    public Vector3 DoorWayPosition;

    private bool playerInRange;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);    
    }

    private void Update()
    {
        if(playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if(InputManager.Instance.GetInteractPressed())
            {
                Interact();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
         if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void Interact()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {   
            _PlayerManager.Instance.transform.position = DoorWayPosition;
            // PlayerManager.Instance.AddCurrentFatigue(-1);
            // PlayerManager.Instance.AddCurrentHunger(-1);
            // TimeManager.Instance.increaseMinute(5);
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("No target scene name specified for the door.");
        }
    }
}
