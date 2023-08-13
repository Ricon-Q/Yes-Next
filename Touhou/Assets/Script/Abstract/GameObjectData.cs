using UnityEngine;

public abstract class GameObjectData
{
    public string ObjectType;

    public GameObjectData(string objectType)
    {
        ObjectType = objectType;
    }
}
