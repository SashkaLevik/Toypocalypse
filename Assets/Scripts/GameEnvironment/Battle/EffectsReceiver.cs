using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class EffectsReceiver : MonoBehaviour
    {        
        [SerializeField] protected List<ReceivedEffect> _receivedEffects;
        [SerializeField] protected Image _resist;

        private int _effectPercent;
        protected float _animationDelay = 0.8f;
        protected IHealth _health;
        protected BaseEnemy _enemy;
        protected EnemyHealth _enemyHealth;
        protected EnemyAI _enemyAI;
        protected EnemyMovement _enemyMovement;

        protected Toy _player;
        protected PlayerMovement _playerMovement;
        protected PlayerHealth _playerHealth;
        protected SkillPanel _skillPanel;        

        public void TryApplyEffect(SkillData skillData)
        {
            _effectPercent = Random.Range(0, 11);

            StartCoroutine(ApplyEffect(skillData));            
        }

        private IEnumerator ApplyEffect(SkillData skillData)
        {
            yield return new WaitForSeconds(_animationDelay);

            if (_effectPercent <= skillData.EffectChance)
                Receive(skillData);
            else
                StartCoroutine(ShowResist());                        
        }

        private IEnumerator ShowResist()
        {
            _resist.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _resist.gameObject.SetActive(false);
        }

        protected virtual void ChekTrauma()
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillData != null && effect.SkillData.AttackType == AttackType.Trauma)
                {
                    _health.TakeDamage(effect.SkillData.EffectValue);
                }
            }
        }

        protected virtual void CheckEffects()
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillData != null)
                {
                    effect.Apply(_health);
                }
            }
        }               

        private void Receive(SkillData skillData)
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillData != null && effect.SkillData == skillData)
                {
                    effect.Stack(skillData);
                    return;
                }                    
                else if (effect.SkillData == null)
                {
                    effect.InitData(skillData);
                    return;
                }
            }
        }
    }
}
