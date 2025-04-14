using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class Player : Plane
    {

        [SerializeField]
        private int _healthAndMag = 10;

        [SerializeField]
        private int _maxAmmo = 45; //Can be 30 - 45 - 60. TO TEST

        private int _ammo;

        public int GetHealth() => _healthAndMag;

        public int GetAmmo() => _ammo;

        private void Start()
        {
            _ammo = _maxAmmo;
        }

        private void OnEnable()
        {
            HealthPack.OnHealthPackCollected += AddHealth;
        }

        private void OnDisable()
        {
            HealthPack.OnHealthPackCollected -= AddHealth;
        }

        private void AddHealth()
        {
            _healthAndMag++;
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
                RefillAmmo();
            }
        }

        public void RefillAmmo()
        {
            _healthAndMag -= 1;
            _ammo = _maxAmmo;
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
