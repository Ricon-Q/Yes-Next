using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSetup : MonoBehaviour
{
    // Start is called before the first frame update
    
    // void Start()
    // {
    //     string sceneName = SceneManager.GetActiveScene().name;

    //     // Debug.Log("Setup Scene " + sceneName);

    //     List<GameObjectData> objectDataList = ObjectManager.Instance.GetObjectsDataInScene(sceneName);

    //     // 오브젝트 생성 또는 정보 적용
    //     foreach (GameObjectData objData in objectDataList)
    //     {
    //         if (objData.obj != null)
    //         {   
    //             // Debug.Log(objData.obj + " is not NULL");
    //             GameObject instantiatedObj = Instantiate(objData.obj, objData.position, Quaternion.identity);
    //             loadData(objData, instantiatedObj);
    //             Debug.Log("Load Object : " + objData.obj);
    //             // 추가 정보 설정...
    //         }
    //         //모든 오브젝트 로드 후 씬 데이터 전체 삭제
    //         ObjectManager.Instance.RemoveAllObjectDataInScene(sceneName);
    //     }
    // }

    // void loadData(GameObjectData objData, GameObject instantiatedObj)
    // {
    //     if (objData.ObjectType == "Crop")
    //     {
    //         var cropData = objData as CropObjectData;
    //         if (cropData != null)
    //         {
    //             Crop cropScript = instantiatedObj.GetComponent<Crop>();
    //             cropScript.SetPlantedDay(cropData.plantedDay);
    //         }
    //     }
    // }
}
