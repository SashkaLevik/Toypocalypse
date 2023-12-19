using Assets.Scripts.UI.Bar;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHud : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private HPBar _hpBar;
        [SerializeField] private SpeedBar _speedBar;

        private void Start()
        {
            UpdateHPBar();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _enemyHealth.CurrentHP -= 1;
            }
            //if (Input.GetKeyDown(KeyCode.Backspace))
            //{
            //    _playerSpeed.CurrentSpeed -= 1;
            //}
        }

        private void OnEnable()
        {
            _enemyHealth.HealthChanged += UpdateHPBar;
        }

        private void UpdateHPBar()
            => _hpBar.SetValue(_enemyHealth.CurrentHP, _enemyHealth.MaxHP);


        //private void UpdateSpeedBar()
        //    => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);

    }
}
