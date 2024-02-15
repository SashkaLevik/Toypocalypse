using Assets.Scripts.Enemyes;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class AttackPanel : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private SkillView _skillView;

        public BaseEnemy _enemy;
        public Toy _player;
        public SkillView _choosedSkill;
        public SkillView _attackSkill;
        public SkillView _nextSkill;

        public AreaType _areaType;
        public PlayerSpeed _playerSpeed;
        public SkillPanel _skillPanel;
        public SkillView AttackSkill => _attackSkill;

        public void Construct(SkillPanel skillPanel)
            => _skillPanel = skillPanel;

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
            if (_choosedSkill != null) ResetSkill();
            _player = GetComponentInParent<Toy>();
            _player.AreaChanged += OnAreaChanged;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
        }

        private void OnDestroy()
        {
            _player.AreaChanged -= OnAreaChanged;
        }

        public void OnSkillChoosed(SkillView skillView)
        {
            _choosedSkill = skillView;
            if(_enemy == null) _enemy = GetComponentInParent<Toy>().Enemy;

            if (_attackSkill == null)
                PrepareSkill();            
        }

        private void PrepareSkill()
        {
            _choosedSkill.transform.SetParent(this.transform);
            _choosedSkill.transform.position = this.transform.position;
            _attackSkill = _choosedSkill;
            _attackSkill.SkillButton.onClick.AddListener(ResetSkill);            
        }        

        public void ApplySkill()
        {
            if (_attackSkill == null)
            {
                _skillPanel.PlayerHud.Warning.Enable(_skillPanel.PlayerHud.Warning.NoSkillWarning);
                return;
            }

            if (CanApply())
            {
                if (_attackSkill.SkillData.SkillType == SkillType.Defence)
                    _player.GetComponent<PlayerHealth>().IncreaseDefence(_attackSkill.SkillData.Defence);
                else
                    ApplyAttack();

                RemovePlayed();
            }
            else
                _skillPanel.PlayerHud.Warning.Enable(_skillPanel.PlayerHud.Warning.AreaWarning);

        }

        private void ApplyAttack()
        {
            _enemy.GetComponent<EnemyHealth>().TakeDamage(_attackSkill.Damage);

            if (_attackSkill.SkillData.AttackType == AttackType.Push)
                _enemy.GetComponent<EnemyMovement>().Push();
            else if (_attackSkill.SkillData.AttackType == AttackType.Pull)
                _enemy.GetComponent<EnemyMovement>().Pull();
            else if (_attackSkill.SkillData.AttackType == AttackType.Stun)
                _enemy.GetComponent<EnemyAI>().CalculateStun(_attackSkill.SkillData.StunChance);
        }

        private void ResetSkill()
        {
            _skillPanel.TakeBack(_attackSkill);
            _attackSkill.SkillButton.onClick.RemoveListener(ResetSkill);
            _attackSkill = null;
        }

        private void RemovePlayed()
        {
            _attackSkill.ApplyCooldown();
            _skillPanel.ReturnPlayed(_attackSkill);
            _attackSkill.SkillButton.onClick.RemoveListener(ResetSkill);
            _attackSkill = null;            
        }

        private void OnAreaChanged(AreaType type)
            => _areaType = type;

        private bool CanApply()
        {
            if (_attackSkill.SkillData.SkillType == SkillType.Defence)
                return true;
            else if (_attackSkill.SkillData.SkillType == SkillType.Minion)
                return true;
            else if (_attackSkill.SkillData.SkillType == SkillType.Melee && _areaType == AreaType.Melee)
                return true;
            else if (_attackSkill.SkillData.SkillType == SkillType.Range && _areaType == AreaType.Range)
                return true;

            return false;
        }
    }
}
