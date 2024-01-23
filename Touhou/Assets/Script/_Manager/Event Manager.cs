using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임의 각종 이벤트 트리거를 관리
// NPC 이벤트는 각 NPC 파일에서 관리 (호감도 이벤트, 첫만남 이벤트 등)
// 각 이벤트는 TriggerOn, EventOn, TriggerOff로 이루어져 있다
// TriggerOn() - 각종 스크립트에서 TriggerOn으로 해당 이벤트가 Trigger되었다는것을 알림
// EventOn() - TriggerOn이 되었다면 함수에서 필요한 시점에 이벤트 발생
// TriggerOff() - Event가 성공적으로 실행되었다면 Trigger Off시킴
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

    public Dictionary<int, Event> _eventDictionary = new Dictionary<int, Event>();

    private Event _innEvent = new InnEvent();

    private void Start() 
    {
        _eventDictionary.Add(0, _innEvent);
    }
    
}
public abstract class Event
{
    public bool _eventSeen;
    public bool _isEventTriggered;

    public Event()
    {
        _eventSeen = false;
        _isEventTriggered = false;
    }
    public abstract void TriggerOn();
    public abstract void EventOn();
    public abstract void TriggerOff();
}

// 플레이어가 집이 아닌 곳에서 피로도가 0 이하가 되었을때 Inn 2층으로 이동후 다음날이 된다.
// 피로도 절반 회복
// 1층으로 이동시 Inn Keeper과 대화후 Inn Fee 소비
public class InnEvent : Event    
{
    public override void TriggerOn()
    {
        if(SceneManager.GetActiveScene().name != "Home")
            _isEventTriggered = true;
    }

    public override void EventOn()
    {
        //
    }

    public override void TriggerOff()
    {
        
    }
}