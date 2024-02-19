using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampLight : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;

    private void Update() 
    {
         if(_TimeManager.Instance.timeData.hour < 6 || 21 <= _TimeManager.Instance.timeData.hour)
            _light2D.intensity = 1;
        else
            _light2D.intensity = 0;
    }
}
