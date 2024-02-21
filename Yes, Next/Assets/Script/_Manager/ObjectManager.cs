using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void RemoveObject(int _placeableItemDataId, Vector3 _position)
    {
        var objectToRemove = _placeableObjects.FirstOrDefault(obj => 
            obj._spawnScene == SceneManager.GetActiveScene().name && 
            obj._placeableItemDataId == _placeableItemDataId &&
            obj._position == _position);

        // 해당 객체가 리스트에 존재한다면 삭제
        if(objectToRemove != null)
        {
            _placeableObjects.Remove(objectToRemove);
        }
    }
}

public class PlaceableObject
{
    public string _spawnScene;
    public int _placeableItemDataId;
    public Vector3 _position;
    public int _previewIndex;

    public PlaceableObject(string _spawnScene, int _placeableItemDataId, Vector3 _position, int _previewIndex)
    {
        Debug.Log(_spawnScene + " : " + _placeableItemDataId + " : " + _position);
        this._spawnScene = _spawnScene;
        this._placeableItemDataId = _placeableItemDataId;
        this._position = _position;
        this._previewIndex = _previewIndex; 
    }
}
