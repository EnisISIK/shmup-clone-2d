using System.Collections;
using UnityEngine;

namespace AlienInvasion
{
    public class ObstacleChunk : Chunk
    {
        [SerializeField]
        private float _chunkLifetime = 10f;

        private void Start()
        {
            StartCoroutine(ChunkLifetimeCoroutine());
        }

        private IEnumerator ChunkLifetimeCoroutine()
        {
            yield return new WaitForSeconds(_chunkLifetime);

            EndOfChunk();

            //Destroy Chunk
        }
    }
}
