using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGardenTrigger : PlaceableTrigger
{
    [Header("Placeable Holder")]
    [SerializeField] private GameObject _object;
    [Header("Item ID")]
    [SerializeField] private int _itemId;
    public override void Interaction()
    {
        ObjectManager.Instance.RemoveObject(_itemId, _object.transform.position);
        Destroy(_object);
    }
}
