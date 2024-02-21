using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SeedPrefab : MonoBehaviour
{
    public _SeedItemData _seedItemData;
    public Vector3 _position;
    public _TimeData _plantedData;

    public void SetData(Vector3 position, _SeedItemData seedItemData, _TimeData today)
    {
        _position = position;
        _seedItemData = seedItemData;
        _plantedData = today;
    }
}
