using UnityEngine;

namespace AlienInvasion
{
    public class SpawnerChunk : Chunk
    {
        BulletWallSpawner _bulletWallSpawner;
        EnemySpawner[] _enemySpawner;

        [SerializeField]
        private float _chunkLifetime = 30f;

        private float _timer = 0f;

        private void Start()
        {
            _bulletWallSpawner = GetComponentInChildren<BulletWallSpawner>();
            _enemySpawner = GetComponentsInChildren<EnemySpawner>();    
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _chunkLifetime)
            {
                EndOfChunk();
            }

            foreach (EnemySpawner enemySpawner in _enemySpawner)
            {
                if (enemySpawner != null)
                    if (!enemySpawner.KillsComplete()) return;
            }

            if (_bulletWallSpawner!=null)
                if (!_bulletWallSpawner.WallsComplete()) return;

            EndOfChunk();
        }

    }
}
