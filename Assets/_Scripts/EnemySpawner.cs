using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace AlienInvasion
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> _enemyTypes;

        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private float _spawnInterval = 2f;

        List<SplineContainer> _splines;
        EnemyFactory _enemyFactory;

        float _spawnTimer;
        int _enemiesSpawned;

        bool _spawningEnabled = false;

        private void OnValidate()
        {
            _splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
        }

        private void Start()
        {
            _enemyFactory = new EnemyFactory();
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += EnableSpawning;
            Enemy.Callback += DecreaseSpawned;
        }

        private void OnDisable()
        {
            Enemy.Callback -= DecreaseSpawned;
        }

        private void DecreaseSpawned()
        {
            _enemiesSpawned--;
        }

        private void EnableSpawning(GameState state)
        {
            _spawningEnabled = true; 
            GameManager.OnGameStateChanged -= EnableSpawning;
        }

        private void Update()
        {
            if (!_spawningEnabled) return;

            _spawnTimer += Time.deltaTime;

            if (_enemiesSpawned < _maxEnemies && _spawnTimer >= _spawnInterval)
            {
                SpawnEnemy();
                _spawnTimer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            EnemyType enemyType = _enemyTypes[UnityEngine.Random.Range(0, _enemyTypes.Count)];
            SplineContainer spline = _splines[UnityEngine.Random.Range(0, _splines.Count)];

            _enemyFactory.CreateEnemy(enemyType, spline);
            _enemiesSpawned++;
        }
    }
}
