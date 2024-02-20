using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using Assets.Scripts.UI.Bar;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BattleHud : MonoBehaviour
    {
        [SerializeField] protected List<AppliedEffect> _appliedEffects;
        [SerializeField] protected List<ReceivedEffect> _receivedEffects;
        [SerializeField] protected HPBar _hpBar;
        [SerializeField] protected SpeedBar _speedBar;
        [SerializeField] protected TMP_Text _defence;
        [SerializeField] protected TMP_Text _damage;        

        protected IHealth _health;
        protected ISpeed _speed;
        protected List<SkillData> _negativeEffects = new();

        protected virtual void Start()
        {
            UpdateHPBar();
        }

        public void ShowAppliedEffect(SkillData skillData)
        {
            foreach (var effect in _appliedEffects)
            {
                if (effect.SkillEffect == null)
                {
                    effect.InitData(skillData);
                    return;
                }
            }
        }

        public void ShowReceivedEffect(SkillData skillData)
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillEffect == null)
                {
                    effect.InitData(skillData);
                    _negativeEffects.Add(skillData);
                    return;
                }
            }
        }

        public void ResetApplied()
        {
            foreach (var effect in _appliedEffects)
                effect.ResetEffect();
        }

        public void ResetReceived()
        {
            _negativeEffects.Clear();

            foreach (var effect in _receivedEffects)
                effect.ResetEffect();
        }

        public bool CheckNegativeEffect(SkillData skillData)
        {
            foreach (var effect in _negativeEffects)
            {
                if (effect.AttackType == skillData.AttackType)
                    return true;
            }

            return false;
        }

        protected virtual void UpdateHPBar()
            => _hpBar.SetValue(_health.CurrentHP, _health.MaxHP);               

        protected virtual void UpdateDefence() { }

    }
}
