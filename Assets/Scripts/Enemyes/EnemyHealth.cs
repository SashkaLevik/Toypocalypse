using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHealth : Health//MonoBehaviour, IHealth
    {
        [SerializeField] private BaseEnemy _enemy;

        public event UnityAction Died;

        private void Start()
        {
            _maxHealth = _enemy.EnemyData.Health;
            _currentHealth = _maxHealth;
            _enemy.AreaChanged += OnDefenceArea;
        }        

        protected override void Die()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}
