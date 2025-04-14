using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    [CreateAssetMenu(menuName = "AlienInvasion/WeaponStrategy/TripleShot", fileName = "TripleShot")]
    public class TripleShot : WeaponStrategy
    {
        [SerializeField] float _spreadAngle = 15f;

        public override void Fire(Transform firePoint, LayerMask layer)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject projectile = Instantiate(_projectilePrefab, firePoint.position, firePoint.rotation);
                projectile.transform.SetParent(firePoint);
                projectile.transform.Rotate(0f, _spreadAngle * (i - 1), 0f);
                projectile.layer = layer;

                var projectileComponent = projectile.GetComponent<Projectile>();
                projectileComponent.SetSpeed(_projectileSpeed);

                Destroy(projectile, _projectileLifetime);
            }
        }
    }
}
