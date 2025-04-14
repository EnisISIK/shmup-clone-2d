using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField]
        private Color _flashColor;

        [SerializeField]
        private float _flashTime;

        private SpriteRenderer _spriteRenderer;
        private Material _material;
        private Coroutine _damageFlashCoroutine;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
            
        }

        private IEnumerator DamageFlash_Coroutine()
        {
            SetFlashColor();

            float currentFlashAmount = 0f;
            float elapsedTime = 0f;
            while (elapsedTime < _flashTime)
            {
                elapsedTime += Time.deltaTime;

                currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / _flashTime));
                SetFlashAmount(currentFlashAmount);

                yield return null;
            }

        }

        public void CallDamageFlash()
        {
            _damageFlashCoroutine = StartCoroutine(DamageFlash_Coroutine());
        }

        private void SetFlashColor()
        {
            _material.SetColor("_FlashColor", _flashColor);
        }

        private void SetFlashAmount(float amount)
        {
            _material.SetFloat("_FlashAmount", amount);
        }
    }
}
