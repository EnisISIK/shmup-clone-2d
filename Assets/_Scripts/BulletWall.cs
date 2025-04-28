using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AlienInvasion
{
    public class BulletWall : MonoBehaviour, IObstacle
    {
        [SerializeField]
        private int _wallHealth;

        [SerializeField]
        private TMP_Text _wallHealthText;

        public int WallHealth() => _wallHealth;

        public static Action Callback;

        void Start()
        {
            originalScale = transform.localScale;
            _wallHealthText.text = _wallHealth.ToString();
        }

        internal void TakeDamage()
        {
            OnHit();
            _wallHealth--;
            _wallHealthText.text = _wallHealth.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);
                Destroy(gameObject, 0.2f);
            }
        }

        private void OnDestroy()
        {
            Callback?.Invoke();
        }

        [SerializeField]
        private float scaleUpFactor = 1.05f;

        [SerializeField]
        private float returnDuration = 0.2f;

        private Vector3 originalScale;
        private Coroutine scaleCoroutine = null;

        public void OnHit()
        {
            if (scaleCoroutine != null)
            {
                StopAllCoroutines();
            }

            scaleCoroutine = StartCoroutine(ScaleUpAndDown());
        }

        private IEnumerator ScaleUpAndDown()
        {
            Vector3 targetScale = originalScale * scaleUpFactor;
            float timeElapsed = 0f;

            transform.localScale = targetScale;

            yield return new WaitForSeconds(0.05f);

            while (timeElapsed < returnDuration)
            {
                transform.localScale = Vector3.Lerp(targetScale, originalScale, timeElapsed / returnDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.localScale = originalScale;
            scaleCoroutine = null;
        }
    }
}
