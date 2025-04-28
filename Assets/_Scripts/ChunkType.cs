using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    [CreateAssetMenu(menuName = "AlienInvasion/ChunkType", fileName = "Chunk")]
    public class ChunkType : ScriptableObject
    {
        private string _chunkName;

        public GameObject Chunk;

        [Range(0f, 1f)]
        public float SpawnWeight = 0.5f;

        public bool FollowCamera;

        //chunk parent class with event callback
        //subclasses invokes the callback when they are over
        //chunkspawner spawn new chunk when callback
        //chunkspawner currentChunk null or not. if null spawn chunk
        //chunkspawner weight bias
    }
}
