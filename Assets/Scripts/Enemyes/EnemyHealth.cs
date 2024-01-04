using Assets.Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHealth : Health//MonoBehaviour, IHealth
    {
        [SerializeField] private BaseEnemy _enemy;
        //[SerializeField] private float _maxHP;
        //[SerializeField] private float _currentHP;

        //public float _defence;

        //public event UnityAction HealthChanged;
        //public event UnityAction DefenceChanged;

        private void Start()
        {
            _maxHealth = _enemy.EnemyData.Health;
            _currentHealth = _maxHealth;
            _enemy.AreaChanged += OnDefenceArea;
        }

        //public float Defence
        //{
        //    get => _defence;
        //    set
        //    {
        //        _defence = value;
        //        DefenceChanged?.Invoke();
        //    }
        //}

        //public float CurrentHP
        //{
        //    get => _currentHP;
        //    set
        //    {
        //        _currentHP = value;
        //        HealthChanged?.Invoke();
        //    }
        //}

        //public float MaxHP { get => _maxHP; set => _maxHP = value; }

        //public void TakeDamage(float damage)
        //{
        //    CurrentHP -= damage;

        //    if (CurrentHP <= 0)
        //        Die();
        //}

        private void Die()
        {
            
        }
    }
}
