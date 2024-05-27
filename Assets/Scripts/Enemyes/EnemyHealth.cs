using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHealth : Health
    {
        [SerializeField] private BaseEnemy _enemy;

        private float _animationDelay = 1.2f;

        public event UnityAction Died;

        private void Start()
        {
            _maxHealth = _enemy.EnemyData.Health;
            _currentHealth = _maxHealth;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            StartCoroutine(Hit());
        }        

        public void IncreaseDefence(float defence)
        {
            Defence += defence;
            _isDefending = true;
        }

        public void DecreaseDefence(float defence)
        {
            Defence -= defence;
            if (Defence < 0) Defence = 0;
            _isDefending = false;
        }

        public void DecreaseDefenceByEffect(float value)
        {
            Defence -= value;
            if (Defence < 0) Defence = 0;
        }            

        protected override void Die()
        {
            StartCoroutine(OnDie());
        }

        private IEnumerator Hit()
        {
            yield return new WaitForSeconds(0.2f);
            _enemy.Animator.PlayHit();            
        }

        private IEnumerator OnDie()
        {
            _enemy.Animator.PlayDie();
            yield return new WaitForSeconds(_animationDelay);
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}
