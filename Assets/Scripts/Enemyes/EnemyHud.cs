using Assets.Scripts.UI;
using Assets.Scripts.UI.Bar;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHud : BattleHud
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemySpeed _enemySpeed;
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        private void OnEnable()
        {
            _enemyHealth.HealthChanged += UpdateHPBar;
            _enemySpeed.SpeedChanged += UpdateSpeedBar;
            _enemyHealth.DefenceChanged += UpdateDefence;
        }

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_enemyHealth.CurrentHP, _enemyHealth.MaxHP);

        protected override void UpdateSpeedBar()
            => _speedBar.SetValue(_enemySpeed.CurrentSpeed, _enemySpeed.MaxSpeed);

        protected override void UpdateDefence()
            => _defence.text = _enemyHealth.Defence.ToString();
    }
}
