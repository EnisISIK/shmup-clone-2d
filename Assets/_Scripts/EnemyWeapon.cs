using UnityEngine;

namespace AlienInvasion
{
    public class EnemyWeapon : Weapon
    {
        float _fireTimer;

        private void Update()
        {
            _fireTimer += Time.deltaTime;

            if (_fireTimer >= weaponStrategy.FireRate)
            {
                weaponStrategy.Fire(firePoint, layer);
                _fireTimer = 0f;
            }
        }
    }

}
