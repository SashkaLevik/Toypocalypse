using Assets.Scripts.Player;
using Assets.Scripts.UI.Bar;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BattleHud : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private HPBar _hpBar;
        [SerializeField] private SpeedBar _speedBar;

        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        private void Start()
        {
            UpdateHPBar();
            UpdateSpeedBar();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerHealth.CurrentHP -= 1;
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _playerSpeed.CurrentSpeed -= 1;
            }
        }

        public void Construct(PlayerHealth playerHealth, PlayerSpeed playerSpeed)
        {
            _playerHealth = playerHealth;
            _playerSpeed = playerSpeed;
            _playerHealth.HealthChanged += UpdateHPBar;
            _playerSpeed.SpeedChanged += UpdateSpeedBar;
        }

        private void UpdateHPBar()
            => _hpBar.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);


        private void UpdateSpeedBar()
            => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);
    }
}
