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
        [SerializeField] private BattleTutorial _battleTutorial;

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
        private EnemyHud _enemyHud;
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
            _player.AreaChanged += OnAreaChanged;
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _enemySpawner.EnemySpawned += GetEnemy;
            _enemySpawner.EnemySpawned += _battleTutorial.OpenTutorial;
        }               

        private void OnDestroy()
        {
            _enemySpawner.EnemySpawned -= GetEnemy;
        }

        public void Construct(EnemySpawner enemySpawner)
            => _enemySpawner = enemySpawner;

        public void PrepareSkills(SkillView skillView)
        {                       
            if (skillView.SkillData.AttackType != AttackType.Simple)
            {
                _appliedEffects.Add(skillView.SkillData);
                _playerHud.ShowAppliedEffect(skillView.SkillData);
                //_preparedSkills.Add(skillView);
            }            
            if (skillView.SkillData.SkillType == SkillType.Defence)
            {
                _preparedSkills.Add(skillView);
                _playerHealth.Defence += skillView.Defence;
                _tempDefence++;
                //if (_playerHud.CheckNegativeEffect(skillView.SkillData))
                //    _playerHealth.Defence += Mathf.Round(skillView.Defence / 2);
                //else
                //    _playerHealth.Defence += skillView.Defence;
            }
            else if (skillView.SkillData.SkillType == SkillType.Attack)
            {
                _preparedSkills.Add(skillView);
                Damage += skillView.Damage;
                _tempDamage++;
                //if (_playerHud.CheckNegativeEffect(skillView.SkillData))
                //    Damage += Mathf.Round(skillView.Damage / 2);
                //else
                //    Damage += skillView.Damage;
            }
        }

        public void Attack()
            => StartCoroutine(ApplyAttack());

        private IEnumerator ApplyAttack()
        {
            _enemyAI.ApplySkillEffects();
            ApplySkillEffects();
            yield return new WaitForSeconds(0.8f);
            _player.Attack();
            _enemyHealth.TakeDamage(_damage);            
            _playerHud.ResetApplied();
            _appliedEffects.Clear();
            _preparedSkills.Clear();
        }

        private void ApplySkillEffects()
        {
            if (_appliedEffects.Count > 0)
            {
                foreach (var effect in _appliedEffects)
                {
                    if (effect.AttackType == AttackType.Push)
                        _enemyMovement.Push();
                    else if (effect.AttackType == AttackType.Pull)
                        _enemyMovement.Pull();
                    else if (effect.AttackType == AttackType.Rust)
                        _enemyHealth.DecreaseDefenceByEffect(effect.NegativeValue);                   
                }
            }
        }                              

        private void GetEnemy(BaseEnemy enemy)
        {
            _enemy = enemy;
            _enemy.AnimationEnded += ResetParameters;
            _enemyHud = _enemy.GetComponentInChildren<EnemyHud>();
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyHealth.Died += Ansigne;
            _enemyMovement = _enemy.GetComponent<EnemyMovement>();
        }

        private void Ansigne()
        {
            _enemy.AnimationEnded -= ResetParameters;
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

        private void OnAreaChanged(AreaType areaType)
        {
            if (areaType == AreaType.Attack)
            {
                foreach (var skill in _preparedSkills)
                {
                    if (skill.SkillData.SkillType == SkillType.Defence)
                        _playerHealth.Defence--;
                    if (skill.SkillData.SkillType == SkillType.Attack)
                        Damage+=2;
                }
            }
            else if (areaType == AreaType.Defence)
            {
                foreach (var skill in _preparedSkills)
                {
                    if (skill.SkillData.SkillType == SkillType.Attack)
                        Damage--;
                    if (skill.SkillData.SkillType == SkillType.Defence)
                        _playerHealth.Defence+=2;
                }
            }
            else if (areaType == AreaType.Common && _player.PreviouseArea == AreaType.Attack)
            {
                foreach (var skill in _preparedSkills)
                {
                    if (skill.SkillData.SkillType == SkillType.Attack)
                        Damage-=2;
                    if (skill.SkillData.SkillType == SkillType.Defence)
                        _playerHealth.Defence++;
                }
            }
            else if (areaType == AreaType.Common && _player.PreviouseArea == AreaType.Defence)
            {
                foreach (var skill in _preparedSkills)
                {
                    if (skill.SkillData.SkillType == SkillType.Defence)
                        _playerHealth.Defence-=2;
                    if (skill.SkillData.SkillType == SkillType.Attack)
                        Damage++;
                }
            }
        }
    }
}
