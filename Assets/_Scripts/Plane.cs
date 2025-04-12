using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public abstract class Plane : MonoBehaviour
    {
        public abstract void TakeDamage(int i);

        public abstract void Die();
    }
}
