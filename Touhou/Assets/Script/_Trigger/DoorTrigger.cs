using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/*/ 
    Door Trigger
    Change Scene
/*/
public class DoorTrigger : MonoBehaviour
{
    [Header("Target Scene")]
    public string targetSceneName; // 변수로 씬 이름을 저장합니다.
    // public Vector3 playerTargetPosition;
    
    [SerializeField] private AreaData targetArea;
    // public Vector2 cameraCenter;
    // public Vector2 mapSize;

    public Vector3 DoorWayPosition;

    private bool playerInRange;
    
    private CameraManager cameraManager = CameraManager.Instance;

    [Header("Time")]
    [SerializeField] private int durationOfMinute = 5;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);    
    }

    private void Update()
    {
        // if(playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
        if(playerInRange)
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
            // FadeInOutManager.Instance.ChangeScene(targetSceneName, DoorWayPosition);
            //  _TimeManager.Instance.increaseMinute(durationOfMinute);
            // // cameraManager.ChangeCameraBorder(cameraCenter, mapSize);
            // _PlayerManager.Instance.playerData.currentArea = targetArea.areaName;
            // // AreaData targetArea = areaDatabase.findArea(areaName);

            // // Camera Setting
            // cameraManager.ChangeCameraBorder(targetArea.areaName);
            // cameraManager.transform.position = DoorWayPosition;

            StartCoroutine(IEnum_Interact());
        }
        else
        {
            Debug.LogWarning("No target scene name specified for the door.");
        }
    }

    // FadeOut - Scene Change - Player Position Change - Camera Position 변경 - time 변환 - Fade In

    public IEnumerator IEnum_Interact()
    {
        FadeInOutManager.Instance.FadeOut();

        yield return new WaitForSeconds(1);

        StartCoroutine(MoveScene());

        // yield return new WaitForEndOfFrame();
        FadeInOutManager.Instance.FadeIn();
    }

    public IEnumerator MoveScene()
    {
        SceneManager.LoadScene(targetSceneName);
        _PlayerManager.Instance.transform.position = DoorWayPosition;
        
        _PlayerManager.Instance.playerData.currentArea = targetArea.areaName;
        cameraManager.ChangeCameraBorder(targetArea.areaName);
        cameraManager.transform.position = DoorWayPosition;

        _TimeManager.Instance.increaseMinute(durationOfMinute);

        yield return null;
    }
}
