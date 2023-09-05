using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatigueBar : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    private Slider slider;

    private void Start()
    {  
        slider = GetComponent<Slider>();
    }

    private void Update() 
    {
        slider.value = playerManager.playerData.currentFatigue / playerManager.playerData.maxFatigue;
    }
}
