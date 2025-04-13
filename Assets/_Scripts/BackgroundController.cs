using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class BackgroundController : MonoBehaviour
    {
        private float startPos, length;
        public GameObject cam;
        [SerializeField]
        private float parallaxEffect;

        private void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.y;
        }

        private void Update()
        {
            float distance = cam.transform.position.y * parallaxEffect;
            float movement = cam.transform.position.y * (1 - parallaxEffect);

            transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);

            if(movement > startPos + length)
            {
                startPos += length;
            }
            else if(movement < startPos - length)
            {
                startPos -= length;
            }
        }
    }
}
