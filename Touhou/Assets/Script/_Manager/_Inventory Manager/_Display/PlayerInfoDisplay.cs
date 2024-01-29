using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currHealth;
    [SerializeField] private TextMeshProUGUI maxHealth;
    [SerializeField] private TextMeshProUGUI currStamina;
    [SerializeField] private TextMeshProUGUI maxStamina;
    void Update()
    {
        currHealth.text = _PlayerManager.Instance.playerData.currentHealth.ToString();
        maxHealth.text = _PlayerManager.Instance.playerData.maxHealth.ToString();
        currStamina.text = _PlayerManager.Instance.playerData.currentStamina.ToString();
        maxStamina.text = _PlayerManager.Instance.playerData.maxStamina.ToString();
    }
}