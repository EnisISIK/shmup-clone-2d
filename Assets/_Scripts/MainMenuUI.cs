using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace AlienInvasion
{
    public class MainMenuUI : View
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _optionsButton;
        
        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private Button _healthUpgradeButton;

        [SerializeField]
        private Button _ammoCapacityUpgradeButton;

        [SerializeField]
        private TMP_Text _moneyText;

        public override void Initialize()
        {
            _startButton.onClick.AddListener(() => StartGame());
            _optionsButton.onClick.AddListener(() => ViewManager.Show<OptionsMenuUI>(true));
            _exitButton.onClick.AddListener(() => Helpers.QuitGame());

            _healthUpgradeButton.onClick.AddListener(() => UpgradeHealth());
            _ammoCapacityUpgradeButton.onClick.AddListener(() => UpgradeAmmoCapacity());

            _moneyText.text = GameManager.Instance.Money().ToString();
        }

        private void StartGame()
        {
            ViewManager.Show<InGameMenuUI>(false);

            GameManager.Instance.UpdateGameState(GameState.StartGame);
        }

        private void UpgradeHealth()
        {
            if (GameManager.Instance.Money() < 100) return;

            GameManager.Instance.HandleBuy(UpgradeType.Health);

            _moneyText.text = GameManager.Instance.Money().ToString();
        }

        private void UpgradeAmmoCapacity()
        {
            if (GameManager.Instance.Money() < 100) return;

            GameManager.Instance.HandleBuy(UpgradeType.AmmoCapacity);

            _moneyText.text = GameManager.Instance.Money().ToString();
        }
    }
}
