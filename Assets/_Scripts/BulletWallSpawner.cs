using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class BulletWallSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _wall;

        [SerializeField] private int _maxWalls = 2;
        [SerializeField] private float _spawnInterval = 5f;

        float _spawnTimer;
        int _wallsSpawned;

        private List<GameObject> _walls = new List<GameObject>();
        
        private int _wallsDestroyed;

        public bool WallsComplete() => _wallsDestroyed >= _maxWalls && _wallsSpawned <= 0;

        private void OnEnable()
        {
            BulletWall.Callback += DecreaseSpawned;
        }

        private void OnDisable()
        {
            BulletWall.Callback -= DecreaseSpawned;
        }

        private void Update()
        {
            _spawnTimer += Time.deltaTime;

            if (_wallsSpawned < _maxWalls && _spawnTimer >= _spawnInterval)
            {
                if (_wallsDestroyed >= _maxWalls) return;

                SpawnWall();
                _spawnTimer = 0f;
            }
        }
        private void DecreaseSpawned()
        {
            _wallsSpawned--;
            _wallsDestroyed++;
        }

        private void SpawnWall()
        {
            _walls.Add(Instantiate(_wall, this.transform.position + _wall.transform.position, Quaternion.identity));

            _wallsSpawned++;
        }
    }
}
