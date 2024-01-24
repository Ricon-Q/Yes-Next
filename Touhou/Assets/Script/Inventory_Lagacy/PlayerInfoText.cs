using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currHealth;
    [SerializeField] private TextMeshProUGUI maxHealth;
    [SerializeField] private TextMeshProUGUI currFatigue;
    [SerializeField] private TextMeshProUGUI maxFatigue;
    void Update()
    {
        currHealth.text = _PlayerManager.Instance.playerData.currentHealth.ToString();
        maxHealth.text = _PlayerManager.Instance.playerData.maxHealth.ToString();
        currFatigue.text = _PlayerManager.Instance.playerData.currentStamina.ToString();
        maxFatigue.text = _PlayerManager.Instance.playerData.maxStamina.ToString();
    }
}
