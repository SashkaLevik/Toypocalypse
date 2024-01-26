using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHealth : Health//MonoBehaviour, IHealth
    {
        [SerializeField] private BaseEnemy _enemy;        

        private void Start()
        {
            _maxHealth = _enemy.EnemyData.Health;
            _currentHealth = _maxHealth;
            _enemy.AreaChanged += OnDefenceArea;
        }        

        private void Die()
        {
            
        }
    }
}
