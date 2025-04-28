using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class Obstacle : MonoBehaviour, IObstacle
    {

        public float damageInterval = 2f;
        private Coroutine damageCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                damageCoroutine = StartCoroutine(DamageOverTime(player));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                    damageCoroutine = null;
                }
            }
        }

        private IEnumerator DamageOverTime(Player player)
        {
            while (true)
            {
                player.TakeDamage(1);
                yield return new WaitForSeconds(damageInterval);
            }
        }
    }
}
