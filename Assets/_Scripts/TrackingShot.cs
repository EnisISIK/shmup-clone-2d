using UnityEngine;
using Utilities;

namespace AlienInvasion
{
    [CreateAssetMenu(menuName = "AlienInvasion/WeaponStrategy/PlayerTrackingShot", fileName = "PlayerTrackingShot")]
    public class TrackingShot: WeaponStrategy
    {
        [SerializeField] float _trackingSpeed = 1f;

        Transform target;

        public override void Initialize()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void Fire(Transform firePoint, LayerMask layer)
        {
            GameObject projectile = Instantiate(_projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;

            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(_projectileSpeed);
            projectileComponent.Callback = () =>
            {
                Vector3 direction = (target.position - projectile.transform.position).With(z:firePoint.position.z).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
                projectile.transform.rotation = Quaternion.Slerp(projectile.transform.rotation, rotation, _trackingSpeed * Time.deltaTime);
            };

            Destroy(projectile, _projectileLifetime);
        }
    }

}
