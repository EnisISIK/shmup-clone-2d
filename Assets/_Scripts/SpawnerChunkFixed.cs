using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class SpawnerChunkFixed : Chunk
    {
        BulletWallSpawner _bulletWallSpawner;

        [SerializeField]
        private List<GameObject> _enemies;

        [SerializeField]
        private float _chunkLifetime = 20f;

        private int _enemyCount;

        private float _timer = 0f;

        private void OnEnable()
        {
            Enemy.Callback += DecreaseEnemyCount;
        }

        private void OnDisable()
        {
            Enemy.Callback -= DecreaseEnemyCount;
        }

        private void DecreaseEnemyCount()
        {
            _enemyCount--;
        }

        private void Start()
        {
            _bulletWallSpawner = GetComponentInChildren<BulletWallSpawner>();
            _enemyCount = GetComponentsInChildren<Enemy>().Length;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _chunkLifetime)
            {
                EndOfChunk();
            }

            if (_enemyCount > 0)
                return;

            if (_bulletWallSpawner != null)
                if (!_bulletWallSpawner.WallsComplete()) return;

            EndOfChunk();
        }
    }
}
