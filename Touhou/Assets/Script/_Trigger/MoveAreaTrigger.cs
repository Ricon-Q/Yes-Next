using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAreaTrigger : MonoBehaviour
{   
    [Header("Area")]
    [SerializeField] private Vector3 playerPosition;
    // [SerializeField] private AreaDatabase areaDatabase;
    [SerializeField] private AreaData targetArea;
    // [SerializeField] private string areaToMove;
    private CameraManager cameraManager = CameraManager.Instance;

    private bool playerInRange;

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
        if(playerInRange)
        {
            visualCue.SetActive(true);
            if(InputManager.Instance.GetInteractPressed())
            {
                // MoveArea(areaToMove);
                StartCoroutine(IEnum_Interact());
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
    // public void MoveArea()
    // {
    //     _TimeManager.Instance.increaseMinute(durationOfMinute);
    //     _PlayerManager.Instance.transform.position = playerPosition;
    //     _PlayerManager.Instance.playerData.currentArea = targetArea.areaName;
    //     // AreaData targetArea = areaDatabase.findArea(areaName);

    //     cameraManager.ChangeCameraBorder(targetArea.areaName);
    //     cameraManager.transform.position = playerPosition;

    // }

    // FadeOut - Player Position Change - Camera Position 변경 - time 변환 - Fade In

    public IEnumerator IEnum_Interact()
    {
        FadeInOutManager.Instance.FadeOut();

        yield return new WaitForSeconds(1);

        StartCoroutine(MoveArea());
        
        FadeInOutManager.Instance.FadeIn();
    }

    public IEnumerator MoveArea()
    {
        _PlayerManager.Instance.transform.position = playerPosition;
        _PlayerManager.Instance.playerData.currentArea = targetArea.areaName;
        
        cameraManager.ChangeCameraBorder(targetArea.areaName);
        cameraManager.transform.position = playerPosition;

        _TimeManager.Instance.increaseMinute(durationOfMinute);

        yield return null;
    }
}
