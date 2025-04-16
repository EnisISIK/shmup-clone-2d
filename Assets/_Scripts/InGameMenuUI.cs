using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace AlienInvasion
{
    public class InGameMenuUI : View
    {
        [SerializeField]
        private TMP_Text _scoreText;
        
        [SerializeField]
        private TMP_Text _healthText;

        private int _ammoCapacity;

        [SerializeField]
        private TMP_Text _ammoText;

        public override void Initialize()
        {
            _scoreText.text = " 0 ";

            GameManager.OnGameStateChanged += InitializeUI;

        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= InitializeUI;
        }

        private void InitializeUI(GameState state)
        {
            if (state == GameState.GameOver) return;

            _ammoCapacity = GameManager.Instance.Player.GetAmmoCapacity();
            _healthText.text = GameManager.Instance.Player.GetHealth().ToString();
            _ammoText.text = GameManager.Instance.Player.GetAmmo().ToString() + " / " + _ammoCapacity;
        }

        private void OnEnable()
        { 
            Player.OnHealthChange += UpdateHealthText;
            Player.OnAmmoChange += UpdateAmmoText;
        }

        private void OnDisable()
        {
            Player.OnHealthChange -= UpdateHealthText;
            Player.OnAmmoChange -= UpdateAmmoText;
        }

        private void UpdateHealthText(int healthAndMagCount)
        {
            _healthText.text = healthAndMagCount.ToString();
        }

        private void UpdateAmmoText(int ammoCount)
        {
            _ammoText.text = ammoCount.ToString() + " / " + _ammoCapacity;
        }

        private void Update()
        {
            _scoreText.text = GameManager.Instance.Score().ToString();
        }
    }
}
