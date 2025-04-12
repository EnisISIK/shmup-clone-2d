using UnityEngine;

namespace AlienInvasion
{
    public class Enemy : Plane
    {
        [SerializeField]
        private int _health;

        public override void Die()
        {
            Destroy(gameObject);
        }

        public override void TakeDamage(int amount)
        {
            _health -= amount;
            if(_health == 0)
            {
                Die();
            }
        }
    }
}
