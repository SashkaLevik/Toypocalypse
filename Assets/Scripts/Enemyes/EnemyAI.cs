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
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyHud _enemyHud;

        private int _randomSkill;
        private int _skillsPerRound = 3;
        private int _stunValue;
        private float _damage;
        private int _randomArea;
        private bool _isInAttackArea;
        private float _prepareSkillDelay = 0.2f;
        private float _appearDelay = 1.4f;
        public AreaType _areaType;
        private Toy _player;
        private EffectsReceiver _playerEffects;
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private PlayerHud _playerHud;
        private SkillData _skillData;
        private SkillData _currentSkill;
        private EnemyMovement _movement;
        private EnemySpeed _enemySpeed;
        private AnimatorController _animator;
        private List<SkillData> _appliedEffects = new();
        private List<SkillData> _preparedSkills = new();
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
        public event UnityAction SkillPlayed;

        private void Start()
        {
            _player = _enemy.Player;
            _playerEffects = _player.GetComponent<EffectsReceiver>();
            _playerHud = _player.PlayerHud;
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _movement = GetComponent<EnemyMovement>();
            _enemyHud = _enemy.EnemyHud;
            _animator = _enemy.Animator;
            Invoke(nameof(PrepareSkills), _appearDelay);
        }                                     

        public void PrepareSkills()
            => StartCoroutine(ChooseSkills());                      

        public void EnemyTurn()
        {
            StartCoroutine(ApplySkill());
        }

        public void EndTurn()
        {
            StartCoroutine(ApplyRemaining());
        }      

        private IEnumerator ApplyRemaining()
        {
            if (_preparedSkills.Count > 0)
            {
                while (_preparedSkills.Count != 0)
                {
                    yield return StartCoroutine(ApplySkill());
                }
            }

            yield return new WaitForSeconds(0.2f);

            yield return StartCoroutine(ChooseSkills());
        }

        private IEnumerator ChooseSkills()
        {
            ChangeArea();

            _shaffledSkills = _enemy.EnemyData.Skills.OrderBy(x => Random.value).ToList();

            for (int i = 0; i < _skillsPerRound; i++)
            {
                yield return new WaitForSeconds(_prepareSkillDelay);
                _skillData = _shaffledSkills[i];
                _enemyHud.ShowAppliedEffect(_skillData);
                _preparedSkills.Add(_skillData);
            }
        }

        private IEnumerator ApplySkill()
        {
            if (_preparedSkills.Count > 0)
            {
                _currentSkill = _preparedSkills[0];
                yield return new WaitForSeconds(1f);

                if (_currentSkill.AttackType == AttackType.Push)
                    _playerMovement.Push();
                else if (_currentSkill.AttackType == AttackType.Pull)
                    _playerMovement.Pull();
                else if (_currentSkill.AttackType == AttackType.Rust)
                    _playerHealth.DecreaseDefence(_currentSkill.EffectValue);
                else if (_currentSkill.AttackType == AttackType.Trauma)
                    _playerEffects.Receive(_currentSkill);

                if (_currentSkill.SkillType == SkillType.Defence)
                    _enemyHealth.Defence += _currentSkill.Defence;
                else if (_currentSkill.SkillType == SkillType.Attack)
                {
                    Damage += _currentSkill.Damage;
                    if(_animator.IsDead == false)
                        yield return StartCoroutine(Attack());
                }

                _preparedSkills.Remove(_preparedSkills[0]);
                _enemyHud.ResetEffect();
            }

            SkillPlayed?.Invoke();
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(0.5f);
            _animator.PlayAttack(_currentSkill);
            _player.GetComponent<PlayerHealth>().TakeDamage(_damage);
            ResetDamage(_enemy.Area);
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
                Damage = area.IncreasedValue;
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
