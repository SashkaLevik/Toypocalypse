using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.GameEnvironment.Dice;
using Assets.Scripts.UI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHud : BattleHud
    {        
        [SerializeField] private BattleHudWarning _warning;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private AttackPanel _attackPanel;
        [SerializeField] private AreaDice _dicePrefab;
        [SerializeField] private Button _areaDice;
        [SerializeField] private Area _commonArea;
        [SerializeField] private Area _defenceArea;
        [SerializeField] private Area _attackArea;
        [SerializeField] private Area[] _areas;

        private int _rerolls;
        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;
        private RoutMap _routMap;
        private AreaDice _dice;

        public Toy Player => _player;
        public Area[] Areas => _areas;
        public BattleHudWarning Warning => _warning;

        public event UnityAction<Area> AreaChanged;

        private void Awake()
            => _canvas.worldCamera = Camera.main;

        private void Start()
        {
            UpdateHPBar();
            UpdateSpeedBar();
            _dice = Instantiate(_dicePrefab);
            _dice.OnDiceResult += SetBattleArea;
            _rerolls = _dice.Rerolls;
            _areaDice.onClick.AddListener(RollDice);
        }

        private void RollDice()
        {
            _dice.Roll();
            //_rerolls--;

            if (_rerolls == 0)
                _areaDice.interactable = false;
        }

        private void SetBattleArea(AreaType areaType)
        {
            foreach (var area in _areas)
                area.gameObject.SetActive(false);

            if (areaType == AreaType.Attack)
            {
                _attackArea.gameObject.SetActive(true);
                AreaChanged?.Invoke(_attackArea);
            }
            else if (areaType == AreaType.Defence)
            {
                _defenceArea.gameObject.SetActive(true);
                AreaChanged.Invoke(_defenceArea);
            }
            else if (areaType == AreaType.Common)
            {
                _commonArea.gameObject.SetActive(true);
                AreaChanged.Invoke(_commonArea);
            }
        }

        public void ResetDice()
        {
            _areaDice.interactable = true;
            _rerolls = _dice.Rerolls;
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

        public void ShowIcons()
        {
            _attackAnimator.gameObject.SetActive(true);
            _defenceAnimator.gameObject.SetActive(true);
        }

        public void HideIcons()
        {
            _attackAnimator.gameObject.SetActive(false);
            _defenceAnimator.gameObject.SetActive(false);
        }

        public void ShowOnLeft(AreaType areaType)
        {
            float defaoultDamage = _attackPanel.Damage - _attackPanel.TempDamage * 2;
            float defaultDefence = _playerHealth.Defence +_attackPanel.TempDefence;
            float tempDamage = _attackPanel.Damage - _attackPanel.TempDamage;
            float tempDefence = _playerHealth.Defence + _attackPanel.TempDefence * 2;

            if (areaType == AreaType.Common)
            {
                if (_attackPanel.TempDamage > 0)
                {
                    _damage.color = Color.red;
                    _damage.text = tempDamage.ToString();
                }
                if (_attackPanel.TempDefence > 0)
                {
                    _defence.color = Color.green;
                    _defence.text = tempDefence.ToString();
                }
            }
            else if (areaType == AreaType.Attack)
            {
                if (_attackPanel.TempDamage > 0)
                {
                    _damage.color = Color.red;
                    _damage.text = defaoultDamage.ToString();
                }
                if (_attackPanel.TempDefence > 0)
                {
                    _defence.color = Color.green;
                    _defence.text = defaultDefence.ToString();
                }
            }
        }

        public void ShowOnRight(AreaType areaType)
        {
            float defaoultDamage = _attackPanel.Damage + _attackPanel.TempDamage;
            float defaultDefence = _playerHealth.Defence - _attackPanel.TempDefence * 2;
            float tempDamage = _attackPanel.Damage + _attackPanel.TempDamage * 2;
            float tempDefence = _playerHealth.Defence - _attackPanel.TempDefence;

            if (areaType == AreaType.Common)
            {
                
                if (_attackPanel.TempDamage > 0)
                {
                    _damage.color = Color.green;
                    _damage.text = tempDamage.ToString();
                }
                if (_attackPanel.TempDefence > 0)
                {
                    _defence.color = Color.red;
                    _defence.text = tempDefence.ToString();
                }
            }
            else if (areaType == AreaType.Defence)
            {
                if (_attackPanel.TempDamage > 0)
                {
                    _damage.color = Color.green;
                    _damage.text = defaoultDamage.ToString();
                }
                if (_attackPanel.TempDefence > 0)
                {
                    _defence.color = Color.red;
                    _defence.text = defaultDefence.ToString();
                }
            }
        }

        public void SetDefault()
        {
            _damage.color = Color.white;
            _defence.color = Color.white;
            _damage.text = _attackPanel.Damage.ToString();
            _defence.text = _playerHealth.Defence.ToString();            
        }        

        private void UpdateDefence()
        {
            _defence.text = _playerHealth.Defence.ToString();
            _defenceAnimator.Play();
        }

        private void UpdateDamage()
        {
            _damage.text = _attackPanel.Damage.ToString();
            _attackAnimator.Play();
        }

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);

        protected void UpdateSpeedBar()
            => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);        
    }
}
