using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _preloadCountPool;
    private GameObjectPool _pool;
    [SerializeField] Transform[] _spawnPoints;
    private int _currentActiveEnemy;
    private int _maxSpawnCurrent;
    [SerializeField] private float _intervalAddedMaxSpawn = 30f;
    private WaitForSeconds _waitForSeconds;
    public Action ReturnToPool;

    [SerializeField] private Tutorial _tutorial;
    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_intervalAddedMaxSpawn);
        _maxSpawnCurrent = _preloadCountPool;
        _pool = new GameObjectPool(_enemyPrefab,_preloadCountPool);
    }

    private void OnEnable()
    {
        _tutorial.EndTutorial += StartGame;
    }

    private void OnDisable()
    {
        _tutorial.EndTutorial -= StartGame;
    }

    private void StartGame()
    {
        StartCoroutine(AddedMaxSpawn());
        Spawn(_preloadCountPool);
    }
    
    void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var enemy = _pool.Get();
            var enemyController = enemy.GetComponent<EnemyControler>();
            enemyController.spawn = this;
            enemyController.ResetState();
            enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position 
                                       + new Vector3(Random.Range(-2, 2), 0, 0);
            _currentActiveEnemy++;
        }
    }

    IEnumerator ReloadSpawn()
    {
        yield return new WaitForSeconds(3f);
        Spawn(_maxSpawnCurrent/2);
    }

    public void Return(GameObject enemy)
    {
        _pool.Return(enemy);
        _currentActiveEnemy--;
        ReturnToPool?.Invoke();
        if (_currentActiveEnemy<_maxSpawnCurrent)
        {
            StartCoroutine(ReloadSpawn());
        }
    }

    IEnumerator AddedMaxSpawn()
    {
        while (Application.isPlaying)
        {
            yield return _waitForSeconds;
            _maxSpawnCurrent++;
        }
    }
    
    
    
    
    
    
}
