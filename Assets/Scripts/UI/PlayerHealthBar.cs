using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Slider healthBar;

    private void Start()
    {
        healthBar = transform.GetComponent<Slider>();
        healthBar.maxValue = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>()?.maxHealth ?? 0;
    }

    public void OnPlayerHealthChange(float health)
    {
        healthBar.value = health;
    }
}
