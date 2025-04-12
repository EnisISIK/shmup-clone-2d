using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private Player _player;
        private int _score;

        public bool IsGameOver() => _player.GetHealth() <= 0;

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
}
