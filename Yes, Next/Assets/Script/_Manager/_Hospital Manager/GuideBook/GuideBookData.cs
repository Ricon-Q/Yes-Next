using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GuideBook Data", menuName = "Hospital System/Guide Book/GuideBook Data")]
public class GuideBookData : ScriptableObject
{
    public string _name;
    [TextArea(5,5)]
    public string _description;
}
