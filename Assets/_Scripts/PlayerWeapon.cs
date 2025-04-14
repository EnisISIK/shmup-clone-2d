﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace AlienInvasion
{
    public class PlayerWeapon : Weapon
    {
        InputReader input;

        Player player;
        float _fireTimer;

        private bool _gameStarted = false;
        private bool _fireSwitch = false;

        private void Awake()
        {
            input = GetComponent<InputReader>();
            player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStart;
        }

        private void OnGameStart(GameState state)
        {
            _gameStarted = true;
            GameManager.OnGameStateChanged -= OnGameStart;
        }

        private void Update()
        {
            if (!_gameStarted) return;
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _fireSwitch = !_fireSwitch;
            }

            _fireTimer += Time.deltaTime;

            Fire();
        }

        private void Fire()
        {

            if (_fireSwitch && _fireTimer >= weaponStrategy.FireRate && player.GetAmmo() > 0)
            {
                player.ReduceAmmo(1);
                weaponStrategy.Fire(firePoint, layer);
                _fireTimer = 0f;
            }
        }
    }

}
