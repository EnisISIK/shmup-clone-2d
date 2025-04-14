using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienInvasion
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;
        [SerializeField] float _smoothness = 0.1f;
        [SerializeField] float _leanAngle = 15f;
        [SerializeField] float _leanSpeed = 5f;


        [SerializeField] GameObject _model;

        [Header("Camera Bounds")]
        [SerializeField] Transform _cameraFollow;
        [SerializeField] float _minX = -8f;
        [SerializeField] float _maxX = 8f;
        [SerializeField] float _minY = -4f;
        [SerializeField] float _maxY = 4f;

        InputReader _input;

        Vector3 _currentVelocity;
        Vector3 _targetPosition;

        private bool _gameRunning = false;
        private float _tempMinX;
        private float _tempMaxX;
        private float _tempMinY;
        private float _tempMaxY;

        private void Start()
        {
            _input = GetComponent<InputReader>();
            AnchorPlayerToCenter();
        }
        
        private void AnchorPlayerToCenter()
        {
            _tempMinY = _minY;
            _tempMaxY = _maxY;
            _tempMinX = _minX;
            _tempMaxX = _maxX;

            _minY = 0;
            _maxY = 0;
            _minX = 0;
            _maxX = 0;
        }


        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStart;
        }

        private void OnGameStart(GameState state)
        {
            _minY = _tempMinY;
            _maxY = _tempMaxY;
            _minX = _tempMinX;
            _maxX = _tempMaxX;

            GameManager.OnGameStateChanged -= OnGameStart;
        }

        private void Update()
        {
            _targetPosition += new Vector3(_input.Move.x, _input.Move.y, 0f) * (_speed * GameManager.Instance.CurrentSpeed() *  Time.deltaTime);

            var minPlayerX = _cameraFollow.position.x + _minX;
            var maxPlayerX = _cameraFollow.position.x + _maxX;
            var minPlayerY = _cameraFollow.position.y + _minY;
            var maxPlayerY = _cameraFollow.position.y + _maxY;

            _targetPosition.x = Mathf.Clamp(_targetPosition.x, minPlayerX, maxPlayerX);
            _targetPosition.y = Mathf.Clamp(_targetPosition.y, minPlayerY, maxPlayerY);


            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, _smoothness);

            var targetRotationAngle = _input.Move.x * _leanAngle;

            var currentYRotation = transform.localEulerAngles.y;
            var newYRotation = Mathf.LerpAngle(currentYRotation, targetRotationAngle, _leanSpeed * Time.deltaTime);

            transform.localEulerAngles = new Vector3(0f, newYRotation, 0f);
        }

    }
}
