using UnityEngine;

namespace AlienInvasion
{
    [CreateAssetMenu(menuName = "AlienInvasion/WeaponStrategy/SingleShot", fileName = "SingleShot")]
    public class SingleShot: WeaponStrategy
    {
        public override void Fire(Transform firePoint, LayerMask layer)
        {
            GameObject projectile = Instantiate(_projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;

            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(_projectileSpeed);

            Destroy(projectile, _projectileLifetime);
        }
    }

}
