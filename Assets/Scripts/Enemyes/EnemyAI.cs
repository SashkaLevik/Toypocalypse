using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Enemyes
{
    public class EnemyAI : MonoBehaviour
    {
        //[SerializeField] private SkillView _skillView;
        [SerializeField] private GameObject _attackPanel;
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private SkillView _stun;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyHud _enemyHud;

        private int _randomSkill;
        private int _stunValue;
        private float _damage;
        private float _constantDamage = 2;
        private int _randomArea;
        private bool _isInAttackArea;
        private float _chooseSkillDelay = 1f;
        public AreaType _areaType;
        private Toy _player;
        private PlayerMovement _playerMovement;
        private PlayerHud _playerHud;
        private SkillData _skillData;
        private SkillData _currentSkill;
        private EnemyMovement _movement;
        private EnemySpeed _enemySpeed;
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
            _player = _enemy.Player;
            _playerHud = _player.PlayerHud;
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _movement = GetComponent<EnemyMovement>();
            _enemy.AreaChanged += OnAreaChanged;
            Invoke(nameof(PrepareSkills), 1.4f);
        }                                     

        public void PrepareSkills()
        {
            StartCoroutine(ChooseSkills());
            ChangeArea();
        }        

        private IEnumerator ChooseSkills()
        {
            for (int i = 0; i < _enemy.EnemyData.Skills.Count - 1; i++)
            {
                _randomSkill = Random.Range(0, _enemy.EnemyData.Skills.Count);
                _skillData = _enemy.EnemyData.Skills[_randomSkill];

                if (_skillData.AttackType != AttackType.Simple)
                {
                    _appliedEffects.Add(_skillData);
                    _enemyHud.ShowAppliedEffect(_skillData);
                }

                if (_skillData.SkillType == SkillType.Defence)
                {
                    if (_enemyHud.CheckNegativeEffect(_skillData))
                        _enemyHealth.Defence += Mathf.Round(_skillData.Defence / 2);
                    else
                        _enemyHealth.Defence += _skillData.Defence;
                }
                else if (_skillData.SkillType == SkillType.Attack)
                {
                    if (_enemyHud.CheckNegativeEffect(_skillData))
                        Damage += Mathf.Round(_skillData.Damage / 2);
                    else
                        Damage += _skillData.Damage;
                }
                yield return new WaitForSeconds(_chooseSkillDelay);

                Debug.Log(_skillData);
            }

            _enemyHud.ResetReceived();
        }        

        public void Attack()
        {
            StartCoroutine(ApplyAttack());
        }

        private IEnumerator ApplyAttack()
        {
            ApplySkillEffects();
            yield return new WaitForSeconds(1f);
            _enemy.PlayAttack();
            _player.GetComponent<PlayerHealth>().TakeDamage(_damage);
            ResetDamage();
            _enemyHealth.ResetDefence();
            _enemyHud.ResetApplied();
            _appliedEffects.Clear();
        }

        private void ApplySkillEffects()
        {
            if (_appliedEffects.Count > 0)
            {
                foreach (var effect in _appliedEffects)
                {
                    if (effect.AttackType == AttackType.Push)
                        _playerMovement.Push();
                    else if (effect.AttackType == AttackType.Pull)
                        _playerMovement.Pull();
                    else
                        _playerHud.ShowReceivedEffect(effect);
                }
            }                           
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
        
        private void ChangeArea()
        {
            _randomArea = Random.Range(1, 4);

            if (_randomArea == 1)
                _movement.MoveLeft();
            else if (_randomArea == 3)
                _movement.MoveRight();            
        }

        private void ResetDamage()
        {
            if (_isInAttackArea)
                Damage = _constantDamage;
            else
                Damage = 0;            
        }

        private void IncreaseDamage(float damage)
            => Damage += damage;

        private void DecreaseDamage(float damage)
        {
            Damage -= damage;
            if (Damage < 0) Damage = 0;
        }

        public void CalculateStun(int stunChance)
        {
            _stunValue = Random.Range(1, 100);
            if (_stunValue < stunChance)
                ApplyStun();
        }

        private void ApplyStun()
        {
            //_waitSkill = Instantiate(_stun, _attackPanel.transform);
        }
    }
}
