using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class Health : MonoBehaviour, IHealth
    {
        protected float _currentHealth;
        protected float _maxHealth;
        protected float _defence;
        protected float _constantDefence = 1;
        protected float _defendingDamage;
        protected bool _isDefending;
        private Animator _animator;
        //public PlayerStats _stats;

        public event UnityAction HealthChanged;
        public event UnityAction DefenceChanged;
        //public event UnityAction Died;

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

        public float MaxHP { get => _maxHealth; set => _maxHealth = value; }
        //{
        //    get => _stats.MaxHP;
        //    set => _stats.MaxHP = value;
        //}

        public void TakeDamage(float damage)
        {
            if (_isDefending)
            {
                _defendingDamage = damage - _defence;

                if (_defendingDamage < 0) _defendingDamage = 0;

                Defence -= damage;
                if (Defence < 1) Defence = 1;

                CurrentHP -= _defendingDamage;
            }
            else
            {
                _defendingDamage = damage - _defence;

                if (_defendingDamage < 0) _defendingDamage = 0;

                Defence -= damage;
                if (Defence < 0) Defence = 0;

                CurrentHP -= _defendingDamage;
            }
        }

        public void IncreaseDefence(float defence)
            => Defence += defence;

        public void DecreaseDefence(float defence)
        {
            if (Defence > 0)
            {
                Defence -= defence;
            }
        }

        protected void OnDefenceArea(AreaType areaType)
        {
            if (areaType == AreaType.Defence)
            {
                IncreaseDefence(_constantDefence);
                _isDefending = true;
            }
            else
            {
                DecreaseDefence(_constantDefence);
                _isDefending = false;
            }
        }
    }
}
