using Assets.Scripts.Data.StaticData;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHud : BattleHud
    {                        
        [SerializeField] private Canvas _canvas;

        private BaseEnemy _enemy;
        private EnemyHealth _enemyHealth;
        private EnemyAI _enemyAI;

        private void Awake()
            => _canvas.worldCamera = Camera.main;

        private void Start()
        {
            UpdateHPBar();
        }        

        private void OnDestroy()
        {
            _enemyHealth.HealthChanged -= UpdateHPBar;
            _enemyHealth.DefenceChanged -= UpdateDefence;
            _enemyAI.DamageChanged -= UpdateDamage;
            _enemyHealth.Died -= DestroyHud;
        }                       

        public void Init(BaseEnemy enemy)
        {
            _enemy = enemy;
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyAI = _enemy.GetComponent<EnemyAI>();            
            _enemyHealth.HealthChanged += UpdateHPBar;
            _enemyHealth.DefenceChanged += UpdateDefence;
            _enemyAI.DamageChanged += UpdateDamage;
            _enemyHealth.Died += DestroyHud;
        }

        private void DestroyHud()
        {
            Destroy(gameObject);
        }

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_enemyHealth.CurrentHP, _enemyHealth.MaxHP);

        private void UpdateDefence()
        {
            _defence.text = _enemyHealth.Defence.ToString();
            _defenceAnimator.Play();
        }            

        private void UpdateDamage()
        {
            _damage.text = _enemyAI.Damage.ToString();
            _attackAnimator.Play();
        }                   
    }
}
