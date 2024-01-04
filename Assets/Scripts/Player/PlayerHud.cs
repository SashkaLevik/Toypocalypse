using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHud : BattleHud
    {
        [SerializeField] private Canvas _canvas;

        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        private void OnDestroy()
        {
            _playerHealth.DefenceChanged -= UpdateDefence;
            _playerHealth.HealthChanged -= UpdateHPBar;
            _playerSpeed.SpeedChanged -= UpdateSpeedBar;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerHealth.TakeDamage(3);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _playerSpeed.CurrentSpeed -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _playerHealth.IncreaseDefence(4);
            }
        }

        public void Construct(PlayerHealth playerHealth, PlayerSpeed playerSpeed)
        {
            _playerHealth = playerHealth;
            _playerSpeed = playerSpeed;
            _playerHealth.DefenceChanged += UpdateDefence;
            _playerHealth.HealthChanged += UpdateHPBar;
            _playerSpeed.SpeedChanged += UpdateSpeedBar;
        }

        protected override void UpdateDefence()
        {
            _defence.text = _playerHealth.Defence.ToString();
        }

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);

        protected override void UpdateSpeedBar()
            => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);
    }
}
