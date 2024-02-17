using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GuideBook Database", menuName = "Hospital System/Guide Book/GuideBook Database")]
public class GuideBookDatabase : ScriptableObject
{
    public List<GuideBookData> _guideBookDatas;
}
