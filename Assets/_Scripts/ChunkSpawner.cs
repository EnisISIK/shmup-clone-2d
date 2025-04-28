using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class ChunkSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<ChunkType> _chunkList;

        private Chunk _chunk;
        private bool _spawningEnabled;

        private float _totalChance = 0f;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += EnableSpawning;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= EnableSpawning;
        }

        private void EnableSpawning(GameState state)
        {
            if (state != GameState.GameOver)
                _spawningEnabled = true;
            else
                _spawningEnabled = false;
        }

        private void Start()
        {
            _totalChance = 0f;
            foreach (ChunkType chunk in _chunkList)
            {
                _totalChance += chunk.SpawnWeight;
            }

            //float rand = Random.Range(0, _totalChance);
            //float cumulativeChance = 0f;

            //foreach (ChunkType chunk in _chunkList)
            //{
            //    cumulativeChance += chunk.SpawnWeight;

            //    if (rand <= cumulativeChance)
            //    {

            //    }
            //}
        }

        private void Update()
        {
            if (!_spawningEnabled) return;

            if (_chunk == null)
            {
                ChunkType randomChunk = GetChunk();//_chunkList[Random.Range(0, _chunkList.Count)];
                GameObject chunkObject;

                if (randomChunk.FollowCamera)
                {
                    chunkObject = Instantiate(randomChunk.Chunk, this.transform);
                }
                else
                {
                    chunkObject = Instantiate(randomChunk.Chunk, Vector3.zero, Quaternion.identity);
                }

                _chunk = chunkObject.GetComponent<Chunk>();
                _chunk.Callback = () =>
                {
                    Destroy(_chunk.gameObject);
                    _chunk = null;
                };
            }
        }

        private ChunkType GetChunk()
        {
            float rand = Random.Range(0, _totalChance);
            float cumulativeChance = 0f;

            foreach (ChunkType chunk in _chunkList)
            {
                cumulativeChance += chunk.SpawnWeight;

                if (rand <= cumulativeChance)
                {
                    return chunk;
                }
            }

            return _chunkList[0];
        }

    }
}
