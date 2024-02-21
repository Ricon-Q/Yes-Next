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
    [SerializeField] private GameObject _line;

    public override void Interaction()
    {
        
        if(!PlantManager.Instance._plantMode)
        {
            _line.SetActive(true);
            PlantManager.Instance.SetPlantMode(true);
            PlantManager.Instance.SetArea(_line.transform.position, 3f, 3f);
            PlayerMovement.SetMoveMode(false);
        }
        else
        {
            _line.SetActive(false);
            PlantManager.Instance.SetPlantMode(false);
            PlantManager.Instance.SetAreaZero();
            PlayerMovement.SetMoveMode(true);
            
        }
        // ObjectManager.Instance.RemoveObject(_itemId, _object.transform.position);
        // Destroy(_object);
    }
}
