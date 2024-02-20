using Assets.Scripts.Data.StaticData;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHud : BattleHud
    {        
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyAI _enemyAI;        
        [SerializeField] private Canvas _canvas;

        private void Awake()
            => _canvas.worldCamera = Camera.main;

        private void OnEnable()
        {
            _enemyHealth.HealthChanged += UpdateHPBar;
            _enemyHealth.DefenceChanged += UpdateDefence;
            _enemyAI.DamageChanged += UpdateDamage;
        }

        private void OnDestroy()
        {
            _enemyHealth.HealthChanged -= UpdateHPBar;
            _enemyHealth.DefenceChanged -= UpdateDefence;
            _enemyAI.DamageChanged -= UpdateDamage;
        }                       

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_enemyHealth.CurrentHP, _enemyHealth.MaxHP);

        protected override void UpdateDefence()
            => _defence.text = _enemyHealth.Defence.ToString();

        private void UpdateDamage()
            => _damage.text = _enemyAI.Damage.ToString();        
    }
}
