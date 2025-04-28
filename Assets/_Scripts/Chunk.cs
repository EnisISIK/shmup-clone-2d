using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class Chunk : MonoBehaviour
    {
        public Action Callback;

        protected void EndOfChunk()
        {
            Callback?.Invoke();
        }
    }
}
