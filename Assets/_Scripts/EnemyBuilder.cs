using UnityEngine;
using UnityEngine.Splines;
using Utilities;

namespace AlienInvasion
{
    public class EnemyBuilder
    {
        private GameObject _enemyPrefab;
        private SplineContainer _spline;
        private GameObject _weaponPrefab;
        private float _speed;

        public EnemyBuilder SetBasePrefab(GameObject prefab)
        {
            _enemyPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpline(SplineContainer spline)
        {
            _spline = spline;
            return this;
        }

        public EnemyBuilder SetWeaponPrefab(GameObject prefab)
        {
            _weaponPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpeed(float speed)
        {

            _speed = speed;
            return this;
        }

        public GameObject Build()
        {
            GameObject instance = GameObject.Instantiate(_enemyPrefab);

            SplineAnimate splineAnimate = instance.GetOrAdd<SplineAnimate>();
            splineAnimate.Container = _spline;
            splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            splineAnimate.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis;
            splineAnimate.ObjectForwardAxis = SplineAnimate.AlignAxis.YAxis;
            splineAnimate.MaxSpeed = _speed;

            splineAnimate.Play();

            instance.transform.position = _spline.EvaluatePosition(0f);

            return instance;
        }

    }
}
