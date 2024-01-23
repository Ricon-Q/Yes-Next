using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 각종 이벤트 트리거를 관리
// NPC 이벤트는 각 NPC 파일에서 관리 (호감도 이벤트, 첫만남 이벤트 등)
// 각 이벤트는 TriggerOn, EventOn, TriggerOff로 이루어져 있다
// TriggerOn() - 각종 스크립트에서 TriggerOn으로 해당 이벤트가 Trigger되었다는것을 알림
// EventOn() - TriggerOn이 되었다면 함수에서 필요한 시점에 이벤트 발생
// TriggerOFf() - Event가 성공적으로 실행되었다면 Trigger Off시킴
public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

// ===================================================================================== //
    

}
