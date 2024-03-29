using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class AttackPanel : MonoBehaviour
    {
        [SerializeField] private PlayerHud _playerHud;

        private float _tempDamage;
        private float _tempDefence;
        private float _damage;        
        private Toy _player;
        private SkillData _currentSkill;
        private PlayerHealth _playerHealth;
        private BaseEnemy _enemy;
        private EnemyAI _enemyAI;
        private EnemyHealth _enemyHealth;
        private EnemySpawner _enemySpawner;
        private EnemyMovement _enemyMovement;
        private AnimatorController _animator;
        private SkillPanel _skillPanel;
        //private EnemyHud _enemyHud;
        private List<SkillData> _appliedEffects = new();
        private List<SkillView> _preparedSkills = new();

        public float Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                DamageChanged?.Invoke();
            }
        }
        public float TempDamage => _tempDamage;
        public float TempDefence => _tempDefence;

        public event UnityAction DamageChanged;

        private void Start()
        {
            _player = _playerHud.Player;
            //_player.AreaChanged += OnAreaChanged;
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _animator = _player.Animator;
            _enemySpawner.EnemySpawned += GetEnemy;
        }               

        private void OnDestroy()
        {
            _enemySpawner.EnemySpawned -= GetEnemy;
        }

        public void Construct(EnemySpawner enemySpawner, SkillPanel skillPanel)
        {
            _enemySpawner = enemySpawner;
            _skillPanel = skillPanel;
        }            

        public void ApplySkill(SkillView skillView)
        {
            _currentSkill = skillView.SkillData;

            if (skillView.SkillData.AttackType == AttackType.Push)
                _enemyMovement.Push();
            else if (skillView.SkillData.AttackType == AttackType.Pull)
                _enemyMovement.Pull();
            else if (skillView.SkillData.AttackType == AttackType.Rust)
                _enemyHealth.DecreaseDefenceByEffect(skillView.SkillData.EffectValue);

            if (skillView.SkillData.SkillType == SkillType.Defence)
            {
                //_preparedSkills.Add(skillView);
                _playerHealth.Defence += skillView.Defence;
                _tempDefence++;
            }
            else if (skillView.SkillData.SkillType == SkillType.Attack)
            {
                Damage += skillView.Damage;
                _tempDamage++;
                Attack();
            }

            _skillPanel.Disactivate();
            _enemyAI.EnemyTurn();
        }       

        public void Attack()
            => StartCoroutine(ApplyAttack());

        private IEnumerator ApplyAttack()
        {
            yield return new WaitForSeconds(0.5f);
            _animator.PlayAttack(_currentSkill);
            _enemyHealth.TakeDamage(_damage);
            Damage = 0;
            //_playerHud.ResetApplied();
            //_appliedEffects.Clear();
            //_preparedSkills.Clear();
        }                                  

        private void GetEnemy(BaseEnemy enemy)
        {
            _enemy = enemy;
            //_enemy.AnimationEnded += ResetParameters;
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyHealth.Died += Ansigne;
            _enemyMovement = _enemy.GetComponent<EnemyMovement>();
        }

        private void Ansigne()
        {
            //_enemy.AnimationEnded -= ResetParameters;
            _enemyHealth.Died -= Ansigne;
            ResetParameters();
        }

        private void ResetParameters()
        {
            Damage = 0;
            _tempDamage = 0;
            _tempDefence = 0;
            _playerHealth.Defence = 0;
        }

        //private void OnAreaChanged(AreaType areaType)
        //{
        //    if (areaType == AreaType.Attack)
        //    {
        //        foreach (var skill in _preparedSkills)
        //        {
        //            if (skill.SkillData.SkillType == SkillType.Defence)
        //                _playerHealth.Defence--;
        //            if (skill.SkillData.SkillType == SkillType.Attack)
        //                Damage+=2;
        //        }
        //    }
        //    else if (areaType == AreaType.Defence)
        //    {
        //        foreach (var skill in _preparedSkills)
        //        {
        //            if (skill.SkillData.SkillType == SkillType.Attack)
        //                Damage--;
        //            if (skill.SkillData.SkillType == SkillType.Defence)
        //                _playerHealth.Defence+=2;
        //        }
        //    }
        //    else if (areaType == AreaType.Common && _player.PreviouseArea == AreaType.Attack)
        //    {
        //        foreach (var skill in _preparedSkills)
        //        {
        //            if (skill.SkillData.SkillType == SkillType.Attack)
        //                Damage-=2;
        //            if (skill.SkillData.SkillType == SkillType.Defence)
        //                _playerHealth.Defence++;
        //        }
        //    }
        //    else if (areaType == AreaType.Common && _player.PreviouseArea == AreaType.Defence)
        //    {
        //        foreach (var skill in _preparedSkills)
        //        {
        //            if (skill.SkillData.SkillType == SkillType.Defence)
        //                _playerHealth.Defence-=2;
        //            if (skill.SkillData.SkillType == SkillType.Attack)
        //                Damage++;
        //        }
        //    }
        //}
    }
}
