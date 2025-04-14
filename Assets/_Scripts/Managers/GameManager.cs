using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState State;

        private Player _player;
        private Transform _cameraTransform;
        private int _score;

        private int _money;

        //GAME SPEEDS UP OVERTIME BASED ON DISTANCE
        private float _currentSpeed = 1;
        private float _speedIncreaseRate = 0.1f;

        private float _distanceTraveledDifficulty = 0;
        private float _lastPositionY;

        //FOR DISTANCE TRAVELED CALCULATIONS
        private float _totalDistance = 0;

        public bool IsGameOver() => _player.GetHealth() <= 0;
        private bool _gameStarted = false;

        public float CurrentSpeed() => _currentSpeed;
        public int Score() => _score;

        public int Money() => _money;

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
        }

        private void HandlePlay()
        {
        }

        private void HandleStartGame()
        {
            _gameStarted = true;
            _lastPositionY = _cameraTransform.position.y;
            UpdateGameState(GameState.Play);
        }

        public void AddScore(int amount)
        {
            _score += amount;
        }

        public int GetScore()
        {
            return _score;
        }

    }

    public enum GameState
    {
        StartGame,
        Play,
        GameOver
    }
}
