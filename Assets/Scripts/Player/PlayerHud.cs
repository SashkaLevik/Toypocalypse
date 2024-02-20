using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHud : BattleHud
    {
        [SerializeField] private BattleHudWarning _warning;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private AttackPanel _attackPanel;

        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;

        public Toy Player => _player;
        public BattleHudWarning Warning => _warning;

        private void Awake()
            => _canvas.worldCamera = Camera.main;

        protected override void Start()
        {
            base.Start();
            UpdateSpeedBar();
        }

        private void OnDestroy()
        {
            _playerHealth.DefenceChanged -= UpdateDefence;
            _playerHealth.HealthChanged -= UpdateHPBar;
            _playerSpeed.SpeedChanged -= UpdateSpeedBar;
            _attackPanel.DamageChanged -= UpdateDamage;
        }

        public void Construct(Toy player, PlayerHealth playerHealth, PlayerSpeed playerSpeed)
        {
            _player = player;
            _playerHealth = playerHealth;
            _playerSpeed = playerSpeed;
            _playerHealth.DefenceChanged += UpdateDefence;
            _playerHealth.HealthChanged += UpdateHPBar;
            _playerSpeed.SpeedChanged += UpdateSpeedBar;
            _attackPanel.DamageChanged += UpdateDamage;
        }                

        protected override void UpdateDefence()
            => _defence.text = _playerHealth.Defence.ToString();

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);

        protected void UpdateSpeedBar()
            => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);

        private void UpdateDamage()
            => _damage.text = _attackPanel.Damage.ToString();
    }
}
