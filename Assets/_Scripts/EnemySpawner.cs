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

        [SerializeField] private int _maxKills = 5;
        private int _killCount = 0;

        List<SplineContainer> _splines;
        EnemyFactory _enemyFactory;

        float _spawnTimer;
        int _enemiesSpawned;

        public int EnemiesAlive() => _enemiesSpawned;

        public bool KillsComplete() => _killCount >= _maxKills && _enemiesSpawned <= 0;

        private void OnValidate()
        {

        }

        private void Start()
        {
            _splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
            _enemyFactory = new EnemyFactory();
        }

        private void OnEnable()
        {
            Enemy.Callback += DecreaseSpawned;
        }

        private void OnDisable()
        {
            Enemy.Callback -= DecreaseSpawned;
        }

        private void DecreaseSpawned()
        {
            _enemiesSpawned--;
            _killCount++;
        }

        private void Update()
        {

            _spawnTimer += Time.deltaTime;

            if (_enemiesSpawned < _maxEnemies && _spawnTimer >= _spawnInterval)
            {
                if (_killCount >= _maxKills) return;

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
