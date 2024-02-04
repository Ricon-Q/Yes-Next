using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeable이 생성되었을때 해당 오브젝트를 저장하여 씬이 변경, 혹은 게임 종료시 저장하는 용도

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;

    public static ObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public List<PlaceableObject> _placeableObjects = new List<PlaceableObject>();
}

public class PlaceableObject
{
    public string _spawnScene;
    public int _placeableItemDataId;
    public Vector3 _position;
    

    public PlaceableObject(string _spawnScene, int _placeableItemDataId, Vector3 _position)
    {
        Debug.Log(_spawnScene + " : " + _placeableItemDataId + " : " + _position);
        this._spawnScene = _spawnScene;
        this._placeableItemDataId = _placeableItemDataId;
        this._position = _position;
    }
}
