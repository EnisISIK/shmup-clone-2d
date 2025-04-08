using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    [CreateAssetMenu(fileName ="EnemyType", menuName = "AlienInvasion/EnemyType", order = 0)]
    public class EnemyType : ScriptableObject
    {
        public GameObject EnemyPrefab;
        public GameObject WeaponPrefab;
        public float Speed =2f;
    }
}
