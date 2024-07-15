using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.Battle;
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
     
        private float _damage;        
        private Toy _player;
        private SkillData _currentSkill;
        private PlayerHealth _playerHealth;
        private BaseEnemy _enemy;
        private EnemyAI _enemyAI;
        private EnemyHealth _enemyHealth;
        private EnemyEffectsReceiver _enemyEffects;
        private PlayerEffectsReceiver _playerEffects;
        private EnemySpawner _enemySpawner;
        private EnemyMovement _enemyMovement;
        private AnimatorController _animator;
        private SkillPanel _skillPanel;
        //private EnemyHud _enemyHud;
        private List<SkillData> _appliedEffects = new List<SkillData>();
        private List<SkillView> _preparedSkills = new List<SkillView>();

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
            _playerEffects = _player.GetComponent<PlayerEffectsReceiver>();
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

            if (_currentSkill.PartType == PartType.Legs)
                StartCoroutine(PlayMoveSkill());                       

            if (_currentSkill.SkillType == SkillType.Defence)
            {
                _playerHealth.Defence += skillView.Defence;
                _playerHud.DefenceAnimator.Play();
            }
            else if (_currentSkill.SkillType == SkillType.Attack)
            {
                _damage += skillView.Damage;
                Attack();
            }

            if (_currentSkill.AttackType != AttackType.Simple && _currentSkill.AttackType != AttackType.Push)
                _enemyEffects.TryApplyEffect(_currentSkill);
        }       

        public void Attack()
            => StartCoroutine(ApplyAttack());      

        private IEnumerator PlayMoveSkill()
        {
            if (_currentSkill.PartType == PartType.Legs)
            {
                _playerHud.RollDice();
                _skillPanel.Disactivate();
                yield return new WaitWhile(() => _playerHud.Dice.IsRolling);
                yield return new WaitForSeconds(0.5f);
                _skillPanel.Activate();
            }            
        }

        private IEnumerator ApplyAttack()
        {
            yield return new WaitForSeconds(0.5f);
            _animator.PlayAttack(_currentSkill);
            _enemyHealth.TakeDamage(_damage);
            _damage = 0;            
        }                                  

        private void GetEnemy(BaseEnemy enemy)
        {
            _enemy = enemy;
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyEffects = _enemy.GetComponent<EnemyEffectsReceiver>();
            _enemyMovement = _enemy.GetComponent<EnemyMovement>();
            _enemyHealth.Died += ResetParameters;
        }      

        private void ResetParameters()
        {
            Damage = 0;          
            _playerHealth.Defence = 0;
            _playerEffects.ResetEffects();
            _enemyHealth.Died -= ResetParameters;
        }
    }
}
