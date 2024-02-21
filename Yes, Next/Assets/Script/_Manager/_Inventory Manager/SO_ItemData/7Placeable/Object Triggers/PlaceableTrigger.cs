using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTrigger : MonoBehaviour
{
    protected bool playerInRange;
    
    [Header("Visual Cue")]
    [SerializeField] protected GameObject visualCue;

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

    public virtual void Interaction()
    {
        
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
}
