using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountKillEnemy : MonoBehaviour
{
    [SerializeField] private Text _countText;
    [SerializeField] SpawnManager _spawnManager;
    private int _count = 0;
    private void OnEnable()
    {
        _spawnManager.ReturnToPool += AddedKill;
    }

    private void OnDisable()
    {
        _spawnManager.ReturnToPool -= AddedKill;
    }

    private void AddedKill()
    {
        _count++;
        _countText.text = _count.ToString();
    }
}
