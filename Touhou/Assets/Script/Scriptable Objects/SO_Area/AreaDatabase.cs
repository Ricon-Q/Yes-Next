using UnityEngine;

[CreateAssetMenu(fileName = "New Area Template", menuName = "Area/Area Database")]
public class AreaDatabase : ScriptableObject
{
    public AreaData[] Areas;

    public AreaData findArea(string areaName)
    {
        foreach (var area in Areas)
        {
            if(areaName == area.areaName)
                return area;
        }
        return null;
    }
}
