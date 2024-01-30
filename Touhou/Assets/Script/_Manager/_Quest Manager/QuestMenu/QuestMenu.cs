using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestMenu : MonoBehaviour
{
    // 퀘스트 메뉴 패널
    // 왼쪽은 선택한 퀘스트의 Description
    // 오른쪽 상단은 플레이어의 퀘스트 리스트
    // 오른쪽 하단은 플레이어의 인벤토리

    [Header("Quest Description Panel")] 
    public TextMeshProUGUI _questName;
    public TextMeshProUGUI _questDescription;
    public TextMeshProUGUI _questDeadline;
    
}
