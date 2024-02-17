using UnityEngine;

[CreateAssetMenu(fileName = "New Area Data", menuName = "Area/Area Data")]

public class AreaData : ScriptableObject
{
    // 구역 이름
    public string areaName;

    // 카메라 센터
    public Vector2 cameraCenter;

    // 맵 사이즈
    public Vector2 mapSize;  
}
