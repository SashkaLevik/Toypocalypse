using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.GameEnvironment.Dice;
using Assets.Scripts.UI;
using System;
using UnityEngine;

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
        private PlayerMovement _movement;
        private Area _currentArea;
        private BattleSystem _battleSystem;

        public Toy Player => _player;
        public Area[] Areas => _areas;
        public BattleHudWarning Warning => _warning;

        private void Awake()
            => _canvas.worldCamera = Camera.main;

        private void Start()
        {
            UpdateHPBar();
            UpdateSpeedBar();
            _currentArea = _commonArea;
        }       

        private void OnDestroy()
        {
            _playerHealth.DefenceChanged -= UpdateDefence;
            _playerHealth.HealthChanged -= _hpBar.OnValueChange;
            _playerSpeed.SpeedChanged -= _speedBar.OnValueChange;
            _dice.OnDiceResult -= SetBattleArea;
            _battleSystem.BattleEntered -= SwitchZone;
        }

        public void Construct(Toy player, BattleSystem battleSystem)
        {
            _battleSystem = battleSystem;
            _player = player;
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _movement = _player.GetComponent<PlayerMovement>();
            _playerHealth.DefenceChanged += UpdateDefence;
            _playerHealth.HealthChanged += _hpBar.OnValueChange;
            _playerSpeed.SpeedChanged += _speedBar.OnValueChange;
            _battleSystem.BattleEntered += SwitchZone;
        }        

        public override void SetBattleArea(DiceFace diceFace)
        {
            foreach (var area in _areas)
                area.gameObject.SetActive(false);

            if (diceFace.FaceType == AreaType.Attack || diceFace.FaceType == AreaType.StrongAttack)
            {
                _attackArea.gameObject.SetActive(true);
                _movement.MoveRight();
                _currentArea = _attackArea;
            }
            else if (diceFace.FaceType == AreaType.Defence || diceFace.FaceType == AreaType.StrongDefence)
            {

                _defenceArea.gameObject.SetActive(true);
                _movement.MoveLeft();
                _currentArea = _defenceArea;
            }
            else if (diceFace.FaceType == AreaType.Common)
            {
                if (_currentArea != _commonArea) _movement.SetDefoultPosition();

                _commonArea.gameObject.SetActive(true);
                _currentArea = _commonArea;
            }
        }

        public void SetCommon()
        {
            if (_player.Animator.IsDead == false)
            {
                foreach (var area in _areas)
                    area.gameObject.SetActive(false);

                if (_movement != null && _currentArea != _commonArea)
                    _movement.SetDefoultPosition();

                _commonArea.gameObject.SetActive(true);
                _currentArea = _commonArea;
            }                      
        }

        private void SwitchZone(bool value)
        {
            if (value == true)
                _commonArea.gameObject.SetActive(true);
            else
                _commonArea.gameObject.SetActive(false);
        }

        private void UpdateDefence()
        {
            _defence.text = _playerHealth.Defence.ToString();
        }       

        public override void UpdateHPBar()
            => _hpBar.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);

        public void UpdateSpeedBar()
            => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);
    }
}
