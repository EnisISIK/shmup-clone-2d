using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class Warning : MonoBehaviour
    {
        [SerializeField]
        private GameObject _obstacle;

        public void ActivateObstacle()
        {
            if (_obstacle != null)
            {
                _obstacle.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}
