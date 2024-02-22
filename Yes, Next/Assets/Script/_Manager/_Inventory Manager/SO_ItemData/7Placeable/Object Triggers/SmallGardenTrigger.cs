using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGardenTrigger : PlaceableTrigger
{
    [Header("Placeable Holder")]
    [SerializeField] private GameObject _object;
    [Header("Item ID")]
    [SerializeField] private int _itemId;

    [Header("Line")]
    public GameObject _line;

    public override void Interaction()
    {
        PlayerMovement.SetMoveMode(false);
        PlantManager.Instance.OpenDestroyGardenPanel();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;

            _line.SetActive(true);
            PlantManager.Instance.SetPlantMode(true);
            PlantManager.Instance.SetArea(_line.transform.position, 3f, 3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collider) 
    {
         if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;

            _line.SetActive(false);
            PlantManager.Instance.SetPlantMode(false);
            PlantManager.Instance.SetAreaZero();
        }
    }
}
