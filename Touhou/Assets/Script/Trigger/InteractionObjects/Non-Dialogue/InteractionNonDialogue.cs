using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNonDialogue : MonoBehaviour
{
    // 상호작용 가능 물건중 대화창이 없는 물건
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
        if(playerInRange)
        {
            visualCue.SetActive(true);
            if(InputManager.Instance.GetInteractPressed())
            {
                Interaction();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    public void Interaction()
    {
        Debug.Log("There is No interaction Function");
    }
}
