using UnityEngine;

namespace AlienInvasion
{
    public class PlayerWeapon : Weapon
    {
        InputReader input;
        float _fireTimer;
        private void Awake()
        {
            input = GetComponent<InputReader>();
        }

        private void Update()
        {
            _fireTimer += Time.deltaTime;

            if (input.Fire && _fireTimer >= weaponStrategy.FireRate)
            {
                weaponStrategy.Fire(firePoint, layer);
                _fireTimer = 0f;
            }
        }
    }

}
