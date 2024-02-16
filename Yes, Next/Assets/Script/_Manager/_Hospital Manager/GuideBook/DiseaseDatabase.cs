using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Disease Database", menuName = "Hospital System/Guide Book/Disease Database")]
public class DiseaseDatabase : ScriptableObject
{
    public List<DiseaseData> _diseaseDatas;
}
