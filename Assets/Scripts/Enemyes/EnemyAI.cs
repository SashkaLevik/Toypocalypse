using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.GameEnvironment.Dice;
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

        private float _damage;

        private EnemySkillView _currentSkill;        
        private Toy _player;
        private PlayerEffectsReceiver _playerEffects;
        private PlayerHealth _playerHealth;
        private AreaDice _dice;
        private AnimatorController _animator;
        private List<EnemySkillView> _preparedSkills = new List<EnemySkillView>();

        //public event UnityAction DamageChanged;
        public event UnityAction EnemyTurnEnded;

        public float Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                //DamageChanged?.Invoke();
            }
        }        

        private void Start()
        {
            _player = _enemy.Player;
            _playerEffects = _player.GetComponent<PlayerEffectsReceiver>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _enemyHud = _enemy.EnemyHud;
            _animator = _enemy.Animator;
        }
        
        private void OnDestroy()
        {
        }        

        public void PrepareSkills()
        {
            _enemyHud.PrepareSkills();
        }                                

        public void EnemyTurn()
            => StartCoroutine(PlaySkills());               

        private IEnumerator PlaySkills()
        {
            _preparedSkills = _enemyHud.PreparedSkills.ToList();

            foreach (var skill in _preparedSkills)
            {
                if (skill.SkillData.PartType == PartType.Legs)
                {
                    _enemyHud.RollDice();
                    yield return new WaitWhile(() => _enemyHud.Dice.IsRolling);
                }
            }

            yield return new WaitForSeconds(0.2f);

            while (_preparedSkills.Count != 0)
            {
                _currentSkill = _preparedSkills[0];


                if (_currentSkill.SkillData.SkillType == SkillType.Defence)
                {
                    _enemyHealth.Defence += _currentSkill.Defence;
                    _enemyHud.DefenceAnimator.Play();
                }                    
                else if (_currentSkill.SkillData.SkillType == SkillType.Attack)
                {
                    Damage = _currentSkill.Damage;
                    if (_animator.IsDead == false)
                        yield return StartCoroutine(Attack());
                }

                if (_currentSkill.SkillData.AttackType != AttackType.Simple && _currentSkill.SkillData.AttackType != AttackType.Push)
                    _playerEffects.TryApplyEffect(_currentSkill.SkillData);
                
                _preparedSkills.Remove(_currentSkill);
                _enemyHud.ResetSkillView();
                yield return new WaitForSeconds(1f);
            }            

            _enemyHud.ResetSkills();
            EnemyTurnEnded?.Invoke();
            _enemyHud.SetCommon();
            Damage = 0;
            PrepareSkills();
        }               

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(0.2f);
            _animator.PlayAttack(_currentSkill.SkillData);

            if(_playerHealth != null)
                _playerHealth.TakeDamage(_damage);
        }
    }
}
