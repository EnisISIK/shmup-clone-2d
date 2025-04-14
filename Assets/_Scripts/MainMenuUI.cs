using System;
using System.Collections;
using System.Collections.Generic;
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

        public override void Initialize()
        {
            _startButton.onClick.AddListener(() => StartGame());
            //_optionsButton.onClick.AddListener(() => ShowOptionsMenu);
            _exitButton.onClick.AddListener(() => Helpers.QuitGame());
        }

        private void StartGame()
        {
            ViewManager.Show<InGameMenuUI>(false);

            GameManager.Instance.UpdateGameState(GameState.StartGame);
        }
    }
}
