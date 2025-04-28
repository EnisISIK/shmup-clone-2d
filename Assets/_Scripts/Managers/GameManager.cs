using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Player Player => _player;

        public GameState State;

        private Player _player;
        private Transform _cameraTransform;
        private int _score;
        private int _sessionMoney = 0;

        private PlayerData _playerData;

        //GAME SPEEDS UP OVERTIME BASED ON DISTANCE
        private float _currentSpeed = 1;

        //FOR DISTANCE TRAVELED CALCULATIONS
        private float _totalDistance = 0;
        private float _distanceTraveledDifficulty = 0;
        private float _lastPositionY;

        public bool IsGameOver() => _player.GetHealth() <= 0;
        private bool _gameStarted = false;

        public float CurrentGameSpeed() => _currentSpeed;
        public int Score() => _score;
        public int Money() => _playerData.money;
        public int SessionMoney() => _sessionMoney;
        public int PlayerHealth() => _playerData.healthUpgrades;
        public int PlayerAmmoCapacity() => _playerData.ammoCapacityUpgrades * 5;


        //EVENTS
        public static event Action<GameState> OnGameStateChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _cameraTransform = Camera.main.gameObject.transform;

            _playerData = GetPlayerData() ?? new PlayerData();
        }

        private void Update()
        {
            if (!_gameStarted) return;


            float distanceThisFrame = _cameraTransform.position.y - _lastPositionY;

            _totalDistance += distanceThisFrame;
            _distanceTraveledDifficulty += distanceThisFrame;

            _score = (int)_totalDistance;


            if(_distanceTraveledDifficulty>=25f)
            {
                _currentSpeed += 0.1f;

                _distanceTraveledDifficulty = 0f;
            }

            _lastPositionY = _cameraTransform.position.y;
        }

        private PlayerData GetPlayerData()
        {
            return JsonHandler.DeserializeData<PlayerData>("PlayerData");
        }

        public void UpdateGameState(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.StartGame:
                    HandleStartGame();
                    break;
                case GameState.Play:
                    HandlePlay();
                    break;
                case GameState.GameOver:
                    HandleGameOver();
                    break;
            }

            OnGameStateChanged?.Invoke(newState);
        }

        private void HandleGameOver()
        {
            _sessionMoney = _score;

            Time.timeScale = 0;

            ViewManager.Show<GameOverMenuUI>(true);

        }

        private void HandlePlay()
        {
            Time.timeScale = 1;
        }

        private void HandleStartGame()
        {
            _gameStarted = true;
            _lastPositionY = _cameraTransform.position.y;
            UpdateGameState(GameState.Play);
        }

        public void HandleBuy(UpgradeType upgrade)
        {
            _playerData.money -= 100;

            switch(upgrade)
            {
                case UpgradeType.Health:
                    _playerData.healthUpgrades++;
                    break;
                case UpgradeType.AmmoCapacity:
                    _playerData.ammoCapacityUpgrades++;
                    break;
            }

            JsonHandler.SerializeData<PlayerData>(_playerData, "PlayerData");
        }

        public void HandleLevelChange()
        {

            JsonHandler.SerializeData<PlayerData>(_playerData, "PlayerData");
        }

        public void DoubleMoneyAd()
        {
            _playerData.money += _sessionMoney * 2;
        }

        public void AddScore(int amount)
        {
            _score += amount;
        }
    }

    public enum GameState
    {
        StartGame,
        Play,
        GameOver
    }
}
