using UnityEngine;

namespace AlienInvasion
{
    public class PlayerWeapon : Weapon
    {
        InputReader input;

        Player player;
        float _fireTimer;

        private bool _gameStarted = false;

        private void Awake()
        {
            input = GetComponent<InputReader>();
            player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            MainMenuUI.Callback += OnGameStart;
        }

        private void OnGameStart()
        {
            _gameStarted = true;
            MainMenuUI.Callback -= OnGameStart;
        }

        private void Update()
        {
            if (!_gameStarted) return;

            _fireTimer += Time.deltaTime;

            Fire();
        }

        private void Fire()
        {
            if (input.Fire && _fireTimer >= weaponStrategy.FireRate && player.GetAmmo() > 0)
            {
                player.ReduceAmmo(1);
                weaponStrategy.Fire(firePoint, layer);
                _fireTimer = 0f;
            }
        }
    }

}
