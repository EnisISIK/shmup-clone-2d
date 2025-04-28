using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class SpawnerChunk : Chunk
    {
        BulletWallSpawner _bulletWallSpawner;
        EnemySpawner _enemySpawner;

        private void Start()
        {
            _bulletWallSpawner = GetComponentInChildren<BulletWallSpawner>();
            _enemySpawner = GetComponentInChildren<EnemySpawner>();    
        }

        private void Update()
        {
            if (_enemySpawner != null) 
                if (!_enemySpawner.KillsComplete()) return;
            
            if(_bulletWallSpawner!=null)
                if (!_bulletWallSpawner.WallsComplete()) return;

            EndOfChunk();
        }

    }
}
