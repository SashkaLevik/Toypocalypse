using Assets.Scripts.UI;
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
        private RoutMap _routMap;

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
            float tempDamage = _attackPanel.Damage - _attackPanel.TempDamage;
            float tempDefence = _playerHealth.Defence + _attackPanel.TempDefence;

            if (areaType != AreaType.Defence)
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
        }

        public void ShowOnRight(AreaType areaType)
        {
            float tempDamage = _attackPanel.Damage + _attackPanel.TempDamage;
            float tempDefence = _playerHealth.Defence - _attackPanel.TempDefence;

            if (areaType != AreaType.Attack)
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
