using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class Player : Plane
    {

        [SerializeField]
        private int _maxHealth = 10;

        private int _healthAndMag;

        [SerializeField]
        private int _maxAmmo = 45; //Can be 30 - 45 - 60. TO TEST

        private int _ammo;

        public int GetHealth() => _healthAndMag;

        public int GetAmmo() => _ammo;

        public int GetAmmoCapacity() => _maxAmmo;

        public static event Action<int> OnHealthChange;
        
        public static event Action<int> OnAmmoChange;

        private bool _diedOnce = false;

        private void OnEnable()
        {
            HealthPack.OnHealthPackCollected += AddHealth;
            GameManager.OnGameStateChanged += Initialize;
        }

        private void OnDisable()
        {
            HealthPack.OnHealthPackCollected -= AddHealth;
            GameManager.OnGameStateChanged -= Initialize;
        }

        private void Initialize(GameState state)
        {
            if (state == GameState.GameOver) return;

            _healthAndMag = _maxHealth + GameManager.Instance.PlayerHealth();
            _maxAmmo = _maxAmmo + GameManager.Instance.PlayerAmmoCapacity();

            _ammo = _maxAmmo;

            if (_diedOnce) _healthAndMag = Mathf.CeilToInt(_healthAndMag / 2);

            OnHealthChange?.Invoke(_healthAndMag);
            OnAmmoChange?.Invoke(_ammo);
        }

        private void AddHealth()
        {
            _healthAndMag++;

            OnHealthChange?.Invoke(_healthAndMag);
        }

        public override void Die()
        {
            _diedOnce = true;
            GameManager.Instance.UpdateGameState(GameState.GameOver);
        }

        public override void TakeDamage(int amount)
        {
            _healthAndMag -= amount;

            OnHealthChange?.Invoke(_healthAndMag);
            DeathCheck();
        }

        public void ReduceAmmo(int amount)
        {
            _ammo -= amount;
            OnAmmoChange?.Invoke(_ammo);
            if (_ammo <= 0)
            {
                if (_healthAndMag <= 1) return;
                RefillAmmo();
            }
        }

        public void RefillAmmo()
        {
            StartCoroutine(RefillAmmoCoroutine());
        }

        IEnumerator RefillAmmoCoroutine()
        {
            yield return new WaitForSeconds(2);

            _healthAndMag -= 1;
            _ammo = _maxAmmo;

            OnHealthChange?.Invoke(_healthAndMag);
            OnAmmoChange?.Invoke(_ammo);
            DeathCheck();
        }

        private void DeathCheck()
        {
            if (_healthAndMag == 0)
            {
                Die();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            IObstacle obstacle = other.GetComponent<IObstacle>();
            if(obstacle != null)
            {
                TakeDamage(2);
            }
        }

    }
}
