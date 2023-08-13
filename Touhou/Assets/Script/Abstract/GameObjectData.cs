using UnityEngine;

public abstract class GameObjectData
{
    // 오브젝트 타입
    // 프리팹 이름
    // 원본 오브젝트 
    // 오브젝트 위치
    // Data 코드 (고유 순서)
    
    public string ObjectType;
    public string name;
    public GameObject obj;
    public Vector3 position;
    public bool isSavedBefore = false;
    // public int code;

    public GameObjectData(string objectType)
    {
        ObjectType = objectType;
    }
}

public class CropObjectData : GameObjectData
{
    // 심어진 날짜
    // 스프라이트 배열

    public int plantedDay;
    // public Sprite[] sprites;

    public CropObjectData() : base("Crop") {}
}
