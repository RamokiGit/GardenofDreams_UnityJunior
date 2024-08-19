using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _health = 100;
    private int _currentHealth;
    public Action OnDeath;
    public Action<int> OnChange;
    
    void Awake()
    {
        _currentHealth = _health;
    }

    public void Damage(int damage)
    {
        if (_currentHealth <= 0) return;
        _currentHealth -= damage;
        if (_currentHealth<=0)
        {
            OnDeath?.Invoke();
        }
        OnChange?.Invoke(damage);
    }

    public void ResetHealth()
    {
        _currentHealth = _health;
    }
}
