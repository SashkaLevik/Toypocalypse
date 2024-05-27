using Assets.Scripts.Data;
using Assets.Scripts.SaveLoad;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : Health, ISaveProgress
    {
        private Toy _player;
        private float _animationDelay = 1.4f;

        public event UnityAction Died;

        private void Awake()
        {
            _player = GetComponent<Toy>();
        }        

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            StartCoroutine(Hit());
        }

        public void Heal(float amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        public void RiseDefence(float amount)
            => Defence += amount;        

        public void DecreaseDefence(float value)
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
            _player.Animator.PlayHit();
            //yield return new WaitForSeconds(0.8f);
            //_player.Animator.PlayHit();
        }

        private IEnumerator OnDie()
        {
            _player.Animator.PlayDie();
            yield return new WaitForSeconds(_animationDelay);
            //yield return StartCoroutine(PlayDieSound());
            Died?.Invoke();
            Destroy(gameObject);
        }

        private IEnumerator PlayDieSound()
        {
            _player.Animator.PlayDie();
            yield return null;
        }

        public void Save(PlayerProgress progress)
        {

        }

        public void Load(PlayerProgress progress)
        {
            _maxHealth = progress.PlayerStats.MaxHP;
            _currentHealth = progress.PlayerStats.CurrentHP;
        }
    }
}
