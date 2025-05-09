using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlienInvasion
{
    public class GameOverMenuUI : View
    {

        [SerializeField]
        private Button _mainMenuButton;

        [SerializeField]
        private TMP_Text _scoreText;

        [SerializeField]
        private TMP_Text _moneyText;

        [SerializeField]
        private Button _playOnButton;

        [SerializeField]
        private Button _doubleMoneyButton;

        public override void Initialize()
        {
            _mainMenuButton.onClick.AddListener(() => LoadLevel());
            _playOnButton.onClick.AddListener(() => PlayOn());
            _doubleMoneyButton.onClick.AddListener(() => DoubleMoney());
        }

        private void OnEnable()
        {
            _scoreText.text = GameManager.Instance.Score().ToString();
            _moneyText.text = GameManager.Instance.SessionMoney().ToString();
        }

        private void DoubleMoney()
        {
            //Lock Button
            //Show Video Ad
            //Update GameManager
            _moneyText.text = (GameManager.Instance.SessionMoney() * 2).ToString();
            GameManager.Instance.DoubleMoneyAd();
        }

        private void PlayOn()
        {
            //Lock Button
            //Show Video Ad
            //Update GameManager State to Play
            ViewManager.ShowLast();
            GameManager.Instance.UpdateGameState(GameState.Play);
        }

        private void LoadLevel()
        {
            GameManager.Instance.HandleLevelChange();
            LevelManager.Instance.LoadScene("Level1");
        }
    }
}
