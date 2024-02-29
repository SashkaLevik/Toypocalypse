using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemyAI : MonoBehaviour
    {
        //[SerializeField] private SkillView _skillView;
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private SkillView _stun;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyHud _enemyHud;

        private int _randomSkill;
        private int _skillsPerRound = 2;
        private int _stunValue;
        private float _damage;
        private int _randomArea;
        private bool _isInAttackArea;
        private float _chooseSkillDelay = 1f;
        public AreaType _areaType;
        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private PlayerHud _playerHud;
        private SkillData _skillData;
        private SkillData _currentSkill;
        private EnemyMovement _movement;
        private EnemySpeed _enemySpeed;
        private List<SkillData> _appliedEffects = new();
        private List<SkillData> _shaffledSkills = new();

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
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _movement = GetComponent<EnemyMovement>();
            Invoke(nameof(PrepareSkills), 1.4f);
        }                                     

        public void PrepareSkills()
        {
            StartCoroutine(ChooseSkills());
        }        

        private IEnumerator ChooseSkills()
        {
            _shaffledSkills = _enemy.EnemyData.Skills.OrderBy(x => Random.value).ToList();

            for (int i = 0; i < _skillsPerRound; i++)
            {                
                yield return new WaitForSeconds(_chooseSkillDelay);

                _skillData = _shaffledSkills[i];

                if (_skillData.AttackType != AttackType.Simple)
                {
                    _appliedEffects.Add(_skillData);
                    _enemyHud.ShowAppliedEffect(_skillData);
                }

                if (_skillData.SkillType == SkillType.Defence)
                    _enemyHealth.Defence += _skillData.Defence;
                else if (_skillData.SkillType == SkillType.Attack)
                    Damage += _skillData.Damage;

                Debug.Log(_skillData);
            }

            ChangeArea();
        }        

        public void Attack()
        {
            StartCoroutine(ApplyAttack());
        }

        public void IncreaseDamage(float damage)
        {
            Damage += damage;
            _isInAttackArea = true;
        }

        public void DecreaseDamage(float damage)
        {
            Damage -= damage;
            _isInAttackArea = false;
            if (Damage < 0) Damage = 0;
        }        

        private IEnumerator ApplyAttack()
        {
            if (_enemy != null)
            {
                yield return new WaitForSeconds(0.8f);
                _enemy.PlayAttack();
                _player.GetComponent<PlayerHealth>().TakeDamage(_damage);
                ResetDamage(_enemy.Area);
                _enemyHealth.ResetDefence(_enemy.Area);
                _enemyHud.ResetApplied();
                _appliedEffects.Clear();
            }            
        }

        public void ApplySkillEffects()
        {
            if (_appliedEffects.Count > 0)
            {
                foreach (var effect in _appliedEffects)
                {
                    if (effect.AttackType == AttackType.Push)
                        _playerMovement.Push();
                    else if (effect.AttackType == AttackType.Pull)
                        _playerMovement.Pull();
                    else if (effect.AttackType == AttackType.Rust)
                        _playerHealth.Defence -= effect.NegativeValue;
                }                    
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

        private void ResetDamage(Area area)
        {
            if (_isInAttackArea)
                Damage = area.AreaBattleValue;
            else
                Damage = 0;            
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
