using Assets.Scripts.UI;
using Assets.Scripts.UI.Bar;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHud : BattleHud
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemySpeed _enemySpeed;
        //[SerializeField] private HPBar _hpBar;
        //[SerializeField] private SpeedBar _speedBar;

        //private void Start()
        //{
        //    UpdateHPBar();
        //}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _enemyHealth.CurrentHP -= 1;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _enemySpeed.CurrentSpeed -= 1;
            }
        }

        private void OnEnable()
        {
            _enemyHealth.HealthChanged += UpdateHPBar;
            _enemySpeed.SpeedChanged += UpdateSpeedBar;
        }

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_enemyHealth.CurrentHP, _enemyHealth.MaxHP);

        protected override void UpdateSpeedBar()
            => _speedBar.SetValue(_enemySpeed.CurrentSpeed, _enemySpeed.MaxSpeed);       
    }
}
