using UnityEngine;

namespace AlienInvasion
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        [SerializeField] int _damage = 10;
        [SerializeField] float _fireRate = 0.5f;
        [SerializeField] protected float _projectileSpeed = 10f;
        [SerializeField] protected float _projectileLifetime = 4f;
        [SerializeField] protected GameObject _projectilePrefab;

        public int Damage => _damage;
        public float FireRate => _fireRate;

        public virtual void Initialize()
        {

        }

        public abstract void Fire(Transform firePoint, LayerMask layer);

    }

}
