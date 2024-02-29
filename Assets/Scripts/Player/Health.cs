using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class Health : MonoBehaviour, IHealth
    {
        protected float _currentHealth;
        protected float _maxHealth;
        protected float _defence;
        protected float _constantDefence = 2;
        protected float _defendingDamage;
        protected bool _isDefending;

        public event UnityAction HealthChanged;
        public event UnityAction DefenceChanged;

        public float Defence
        {
            get => _defence;
            set
            {
                _defence = value;
                DefenceChanged?.Invoke();
            }
        }

        public float CurrentHP
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                HealthChanged?.Invoke();
            }
        }

        public float MaxHP
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                HealthChanged?.Invoke();
            }
        }       

        public virtual void TakeDamage(float damage)
        {
            _defendingDamage = damage - _defence;

            if (_defendingDamage < 0) _defendingDamage = 0;

            Defence -= damage;
            if (Defence < 0) Defence = 0;

            CurrentHP -= _defendingDamage;

            if (CurrentHP < 0) CurrentHP = 0;
            if (CurrentHP <= 0) Die();
        }

        protected virtual void Die() { }                   
    }
}
