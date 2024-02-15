using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHud : BattleHud
    {
        [SerializeField] private BattleHudWarning _warning;
        [SerializeField] private Canvas _canvas;        

        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;
        
        public Toy Player => _player;
        public BattleHudWarning Warning => _warning;

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

        public void Construct(Toy player, PlayerHealth playerHealth, PlayerSpeed playerSpeed)
        {
            _player = player;
            _playerHealth = playerHealth;
            _playerSpeed = playerSpeed;
            _playerHealth.DefenceChanged += UpdateDefence;
            _playerHealth.HealthChanged += UpdateHPBar;
            _playerSpeed.SpeedChanged += UpdateSpeedBar;
        }        

        protected override void UpdateDefence()
            => _defence.text = _playerHealth.Defence.ToString();

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);

        protected override void UpdateSpeedBar()
            => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);                
    }
}
