using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private float _currentHP;
        [SerializeField] private float _maxHP;
        public event UnityAction HealthChanged;

        private void Start()
        {
            _maxHP = _enemy.EnemyData.Health;
            _currentHP = _maxHP;
        }

        public float CurrentHP
        {
            get => _currentHP;
            set
            {
                _currentHP = value;
                HealthChanged?.Invoke();
            }
        }

        public float MaxHP { get => _maxHP; set => _maxHP = value; }

        public void TakeDamage(int damage)
        {
            
        }
    }
}
