// using System.Collections;
// using System.Collections.Generic;
// using Unity.Profiling;
// using UnityEngine;

// public class DialogueTrigger : MonoBehaviour
// {
//     [Header("Visual Cue")]
//     [SerializeField] private GameObject visualCue;

//     [Header("Ink JSON")]
//     [SerializeField] private TextAsset inkJSON;

//     [Header("NPC INFO")]
//     [SerializeField] private NPC npcScript;

//     private bool playerInRange;

//     private void Awake()
//     {
//         playerInRange = false;
//         visualCue.SetActive(false);    
        
//     }

//     private void Update()
//     {
//         if(playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
//         {
//             visualCue.SetActive(true);
//             if(InputManager.Instance.GetInteractPressed() && !ShopManager.Instance.isShopMode)
//             {
//                 DialogueManager.Instance.EnterDialogueMode(inkJSON, npcScript);
//             }
//         }
//         else
//         {
//             visualCue.SetActive(false);
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collider) 
//     {
//         if(collider.gameObject.tag == "Player")
//         {
//             playerInRange = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collider) 
//     {
//          if(collider.gameObject.tag == "Player")
//         {
//             playerInRange = false;
//         }
//     }
// }
