using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Disease Data", menuName = "Hospital System/Guide Book/Disease Data")]
public class DiseaseData : ScriptableObject
{
    public string _diseaseName;
    [TextArea(5, 5)]
    public string _description;

    public List<GuideBookData> _symptomDatas;

}
