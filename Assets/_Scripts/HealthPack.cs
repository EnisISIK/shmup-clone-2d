using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class HealthPack : MonoBehaviour
    {
        public static event Action OnHealthPackCollected;

        private void Start()
        {
            StartCoroutine(HealthPackLifetime());
        }

        private IEnumerator HealthPackLifetime()
        {
            yield return new WaitForSeconds(1f);

            OnHealthPackCollected?.Invoke();

            Destroy(gameObject, 0.5f);
        }

    }
}
