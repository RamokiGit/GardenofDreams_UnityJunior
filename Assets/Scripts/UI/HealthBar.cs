using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthBar;

    private void Awake()
    {
        _healthBar.fillAmount = 1f;
    }

    private void OnEnable()
    {
        _health.OnChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _health.OnChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int value)
    {
        _healthBar.fillAmount -= value/100f;
    }
}
