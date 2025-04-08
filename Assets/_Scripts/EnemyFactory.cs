using UnityEngine;
using UnityEngine.Splines;

namespace AlienInvasion
{
    public class EnemyFactory
    {
        public GameObject CreateEnemy(EnemyType enemyType, SplineContainer spline)
        {
            EnemyBuilder builder = new EnemyBuilder()
                .SetBasePrefab(enemyType.EnemyPrefab)
                .SetSpline(spline)
                .SetSpeed(enemyType.Speed);

            return builder.Build();
        }
    }
}
