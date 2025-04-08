using UnityEngine;

namespace AlienInvasion
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds;
        [SerializeField] float smoothing = 10f;
        [SerializeField] float multiplier = 15f;

        Transform cam;
        Vector3 previousCamPos;

        private void Awake()
        {
            cam = Camera.main.transform;
        }

        private void Start()
        {
            previousCamPos = cam.position;

        }

        private void Update()
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (previousCamPos.y - cam.position.y)*(i*multiplier);

                float targetY = backgrounds[i].position.y + parallax;

                Vector3 backgroundTarget = new Vector3(backgrounds[i].position.x, targetY, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTarget, smoothing * Time.deltaTime);

            }

            previousCamPos = cam.position;
        }

    }
}
