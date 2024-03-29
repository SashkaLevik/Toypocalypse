using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class EffectsReceiver : MonoBehaviour
    {        
        [SerializeField] protected List<ReceivedEffect> _receivedEffects;

        //private List<SkillData> _negativeEffects = new();
        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerMovement _movement;
        private SkillPanel _skillPanel;

        private void Awake()
        {
            _player = GetComponent<Toy>();
            _movement = GetComponent<PlayerMovement>();
            _playerHealth = GetComponent<PlayerHealth>();
            _movement.PlayerMoved += CheckTrauma;
        }

        private void Start()
        {
            _skillPanel = _player.SkillPanel;
            _skillPanel.RoundEnded += ResetEffects;
        }

        private void ResetEffects()
        {
            foreach (var effect in _receivedEffects)
            {
                effect.ReduceDuration();
            }
        }        

        private void CheckTrauma()
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.IsActive() && effect.SkillData.AttackType == AttackType.Trauma)
                    _playerHealth.TakeDamage(effect.EffectDuration);
            }
        }

        public void Receive(SkillData skillData)
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
