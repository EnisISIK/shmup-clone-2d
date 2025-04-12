using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class Player : Plane
    {

        [SerializeField]
        private int _healthAndMag;

        [SerializeField]
        private int _maxAmmo = 45; //Can be 30 - 45 - 60. TO TEST

        private int _ammo;

        public int GetHealth() => _healthAndMag;

        public int GetAmmo() => _ammo;

        private void Start()
        {
            _ammo = _maxAmmo;
        }

        public override void Die()
        {
            throw new System.NotImplementedException();
        }

        public override void TakeDamage(int amount)
        {
            _healthAndMag -= amount;
            DeathCheck();
        }

        public void ReduceAmmo(int amount)
        {
            _ammo -= amount;
            if(_ammo <= 0)
            {
                if (_healthAndMag <= 1) return;
                Debug.Log("Reloading");
                RefillAmmo();
            }
        }

        public void RefillAmmo()
        {
            _healthAndMag -= 1;
            _ammo = _maxAmmo;
            Debug.Log("Player Health : " + _healthAndMag);
            //TODO: Ammo Reference Logic. Probably to use a Callback
            DeathCheck();
        }

        private void DeathCheck()
        {
            if (_healthAndMag == 0)
            {
                Die();
            }
        }

    }
}
