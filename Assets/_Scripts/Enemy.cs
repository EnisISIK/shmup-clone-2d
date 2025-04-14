using System;
using UnityEngine;

namespace AlienInvasion
{
    public class Enemy : Plane
    {
        [SerializeField]
        private int _health;

        public static Action Callback;

        [SerializeField]
        private DamageFlash _damageFlash;

        [SerializeField]
        private GameObject _healthPack;

        public override void Die()
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(_healthPack,transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), 
                                                            UnityEngine.Random.Range(-2f,2f),0), Quaternion.identity);
            }
            Callback?.Invoke();
            Destroy(gameObject);
        }

        public override void TakeDamage(int amount)
        {
            _health -= amount;

            if (_damageFlash != null)
            {
                _damageFlash.CallDamageFlash();
            }

            if(_health == 0)
            {
                Die();
            }
        }
    }
}
