using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// 1. 싱글톤이여야한다
// 2. 각 씬들의 오브젝트들의 정보를 취사선택해야한다
// 3. 각 씬들의 오브젝트가 섞이면 안된다. (예를 들어, 1번씬이라는 배열에는 1번씬의 오브젝트만 담긴다)
// 4. 저장된 오브젝트의 정보들은 원본 오브젝트가 인게임에서 삭제되어도 사라지지 않는다 

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    
    // <씬 이름 - 게임 오브젝트 리스트>로 이루어져 있는 딕셔너리
    [SerializeField] public Dictionary<string, List<GameObjectData>> sceneObjectData = 
        new Dictionary<string, List<GameObjectData>>();


    public static ObjectManager Instance
    {
        get
        {
            if(instance == null)
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
            DontDestroyOnLoad(this.gameObject);
        }    
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddObjectData(string sceneName, GameObjectData data)
    {
        // 오브젝트 데이터 추가
        if(!sceneObjectData.ContainsKey(sceneName))
        {
            sceneObjectData[sceneName] = new List<GameObjectData>();
        }

        sceneObjectData[sceneName].Add(data);
        // Debug.Log
        // (
        // "AddData : " + data.name + 
        // "\n===data info===" + 
        // // "\nDataCode : " + data.code +
        // "\nObjectType : " + data.ObjectType +
        // "\nGameObject : " + data.obj.name + 
        // "\nPosition : " + data.position
        // );
    }

    public void RemoveObjectData(string sceneName, GameObjectData data)
    {
        // 오브젝트 데이터 삭제
        if (sceneObjectData.ContainsKey(sceneName))
        {
            sceneObjectData[sceneName].Remove(data);
        }
    }

    public List<GameObjectData> GetObjectsDataInScene(string sceneName)
    {
        // 씬에 있는 모든 오브젝트 데이터를 리스트 형태로 반환
        if(sceneObjectData.ContainsKey(sceneName))
        {
            return sceneObjectData[sceneName];
        }
        return new List<GameObjectData>();
    }

    public List<GameObjectData> GetAllObjectData()
    {
        // 모든 씬의 오브젝트 데이터를 하나의 리스트 형태로 반환
        return sceneObjectData.Values.SelectMany(list => list).ToList();
    }

    public void RemoveAllObjectDataInScene(string sceneName)
    {
        if (sceneObjectData.ContainsKey(sceneName))
        {
            sceneObjectData.Remove(sceneName); // 해당 씬의 데이터 모두 제거
        }
    }

}
