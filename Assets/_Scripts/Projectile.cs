using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private GameObject _muzzlePrefab;

        [SerializeField]
        private GameObject _hitPrefab;

        private Transform _parent;

        public Action Callback;

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetParent(Transform parent)
        {
            _parent = parent;
        }

        void Start()
        {
            if(_muzzlePrefab != null)
            {
                GameObject muzzleVFX = GameObject.Instantiate(_muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVFX.transform.forward = gameObject.transform.forward;
                muzzleVFX.transform.SetParent(_parent);

                DestroyParticleSystem(muzzleVFX);
            }
        }

        private void Update()
        {
            transform.SetParent(null);
            transform.position += transform.forward * (_speed * GameManager.Instance.CurrentGameSpeed() * Time.deltaTime);

            Callback?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            BulletWall bulletWall = other.GetComponent<BulletWall>();
            if (bulletWall != null)
            {
                if (bulletWall.WallHealth() > 0)
                    bulletWall.TakeDamage();
                else
                {
                    Destroy(bulletWall.gameObject, 0.2f);
                }
            }

            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hitPrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                GameObject hitVFX = GameObject.Instantiate(_hitPrefab, contact.point, Quaternion.identity);

                DestroyParticleSystem(hitVFX);
            }

            var plane = collision.gameObject.GetComponent<Plane>();
            if (plane != null)
            {
                plane.TakeDamage(1);
            }

            Destroy(gameObject);
        }

        private void DestroyParticleSystem(GameObject vfx)
        {
            var ps = vfx.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                ps = vfx.GetComponentInChildren<ParticleSystem>();
            }

            Destroy(vfx, ps.main.duration);
        }
    }
}
