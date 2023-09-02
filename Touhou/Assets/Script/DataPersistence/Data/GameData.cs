using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;

    // 해당 생성자들의 값들은 기본 값이다
    // 데이터에 로드할 것이 없을때 아래 값들을 가져온다
    public GameData()
    {
        this.deathCount = 0;
    }
}
