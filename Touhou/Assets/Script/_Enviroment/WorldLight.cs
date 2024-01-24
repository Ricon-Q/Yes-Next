using UnityEngine;
using UnityEngine.Rendering.Universal;


[RequireComponent(typeof(Light2D))]
public class WorldLight : MonoBehaviour
{
    private Light2D _light;
    [SerializeField] private Gradient _gradient;

    private void Awake() 
    {
        _light = GetComponent<Light2D>();    
    }

    private void Update()
    {
        float timePercentage = _TimeManager.Instance.timeData.hour / 24f; // Normalize the hour to a value between 0 and 1
        _light.color = _gradient.Evaluate(timePercentage); // Set the light color based on the gradient
    }
}
