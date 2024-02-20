using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class AttackPanel : MonoBehaviour
    {
        [SerializeField] private PlayerHud _playerHud;

        private float _damage;
        private float _constantDamage = 2;
        private bool _isInAttackArea;
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

        public float Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                DamageChanged?.Invoke();
            }
        }

        public event UnityAction DamageChanged;

        private void Start()
        {
            _player = _playerHud.Player;
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _player.AreaChanged += OnAreaChanged;
            _enemySpawner.EnemySpawned += GetEnemy;            
        }       

        private void OnDestroy()
        {
            _player.AreaChanged -= OnAreaChanged;
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
                //_currentSkill = skillView.SkillData;
            }
            
            if (skillView.SkillData.SkillType == SkillType.Defence)
            {
                if (_playerHud.CheckNegativeEffect(skillView.SkillData))
                    _playerHealth.Defence += Mathf.Round(skillView.SkillData.Defence / 2);
                else
                    _playerHealth.Defence += skillView.SkillData.Defence;
            }
            else if (skillView.SkillData.SkillType == SkillType.Attack)
            {
                if (_playerHud.CheckNegativeEffect(skillView.SkillData))
                    Damage += Mathf.Round(skillView.SkillData.Damage / 2);
                else
                    Damage += skillView.SkillData.Damage;
            }
        }

        public void Attack()
            => StartCoroutine(ApplyAttack());

        private IEnumerator ApplyAttack()
        {
            _playerHud.ResetReceived();
            ApplySkillEffects();
            yield return new WaitForSeconds(1f);
            _player.PlayAttack();
            _enemyHealth.TakeDamage(_damage);            
            _playerHud.ResetApplied();
            _appliedEffects.Clear();
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
                    else
                        _enemyHud.ShowReceivedEffect(effect);                    
                }
            }
        }        

        private void ResetDamage()
        {
            if (_isInAttackArea)
                Damage = _constantDamage;
            else
                Damage = 0;                        
        }

        private void OnAreaChanged(AreaType areaType)
        {
            if (areaType == AreaType.Attack)
            {
                IncreaseDamage(_constantDamage);
                _isInAttackArea = true;
            }
            else
            {
                DecreaseDamage(_constantDamage);
                _isInAttackArea = false;                
            }
        }

        private void IncreaseDamage(float damage)
            => Damage += damage;

        private void DecreaseDamage(float damage)
        {
            Damage -= damage;
            if (Damage < 0) Damage = 0;
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
        }

        private void ResetParameters()
        {
            ResetDamage();
            _playerHealth.ResetDefence();
        }
    }
}
