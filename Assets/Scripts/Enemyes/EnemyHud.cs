using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.GameEnvironment.Dice;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyHud : BattleHud
    {                        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<EnemySkillView> _skillViews;

        private float _prepareSkillDelay = 0.2f;        
        private BaseEnemy _enemy;
        private EnemyHealth _enemyHealth;
        private EnemyAI _enemyAI;
        private EnemyMovement _movement;
        private Area _currentArea;
        private List<SkillData> _shaffledSkills = new List<SkillData>();
        private List<EnemySkillView> _preparedSkills = new List<EnemySkillView>();

        public List<EnemySkillView> PreparedSkills => _preparedSkills;

        private void Awake()
            => _canvas.worldCamera = Camera.main;

        private void Start()
        {
            UpdateHPBar();
            
        }       

        private void OnDestroy()
        {
            _enemyHealth.HealthChanged -= _hpBar.OnValueChange;
            _enemyHealth.DefenceChanged -= UpdateDefence;
            _enemyHealth.Died -= DestroyHud;
            _dice.OnDiceResult -= SetBattleArea;

            foreach (var skill in _skillViews)
            {
                if (skill != null)
                    _dice.OnDiceResult -= skill.ChangeOnArea;
            }
        }

        public void Init(BaseEnemy enemy)
        {
            _enemy = enemy;
            _movement = _enemy.GetComponent<EnemyMovement>();
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemyHealth.HealthChanged += _hpBar.OnValueChange;
            _enemyHealth.DefenceChanged += UpdateDefence;
            //_enemyAI.DamageChanged += UpdateDamage;
            _enemyHealth.Died += DestroyHud;
        }

        public void SetCommon()
        {
            foreach (var area in _areas)
                area.gameObject.SetActive(false);

            foreach (var skill in _skillViews)
                skill.ResetArea();

            if (_enemy.Animator.IsDead == false)
            {               
                if (_currentArea != _commonArea) _movement.SetDefoultPosition();

                _commonArea.gameObject.SetActive(true);
                _currentArea = _commonArea;
            }            
        }

        public void ResetSkillView()
        {
            foreach (var skill in _skillViews)
            {
                if (skill.SkillData != null)
                {
                    skill.Reset();
                    return;
                }
            }
        }

        public void ResetSkills()
            => _preparedSkills.Clear();

        public void PrepareSkills()
        {
            StartCoroutine(ChooseSkills());            
        }

        private IEnumerator ChooseSkills()
        {
            _shaffledSkills = _enemy.EnemyData.Skills.OrderBy(x => Random.value).ToList();

            foreach (var skill in _shaffledSkills)
            {
                if (skill.PartType == PartType.Legs)
                {
                    if(_dice == null)
                        InstantiateDice(skill.AreaDice);
                }
            }

            for (int i = 0; i < _enemy.SkillsAmount; i++)
            {
                yield return new WaitForSeconds(_prepareSkillDelay);

                _skillViews[i].InitData(_shaffledSkills[i]);
                _preparedSkills.Add(_skillViews[i]);
                _dice.OnDiceResult += _skillViews[i].ChangeOnArea;
            }            
        }              

        public override void SetBattleArea(DiceFace diceFace)
        {
            foreach (var area in _areas)
                area.gameObject.SetActive(false);

            if (diceFace.FaceType == AreaType.Attack || diceFace.FaceType == AreaType.StrongAttack)
            {
                _attackArea.gameObject.SetActive(true);
                _movement.MoveLeft();
                _currentArea = _attackArea;
            }
            else if (diceFace.FaceType == AreaType.Defence || diceFace.FaceType == AreaType.StrongDefence)
            {

                _defenceArea.gameObject.SetActive(true);
                _movement.MoveRight();
                _currentArea = _defenceArea;
            }
            else if (diceFace.FaceType == AreaType.Common)
            {
                if (_currentArea != _commonArea) _movement.SetDefoultPosition();

                _commonArea.gameObject.SetActive(true);
                _currentArea = _commonArea;
            }
        }        

        private void DestroyHud()
        {
            Destroy(gameObject);
        }

        public override void UpdateHPBar()
            => _hpBar.SetValue(_enemyHealth.CurrentHP, _enemyHealth.MaxHP);

        private void UpdateDefence()
        {
            _defence.text = _enemyHealth.Defence.ToString();
        }            

        private void UpdateDamage()
        {
            _damage.text = _enemyAI.Damage.ToString();
            //_attackAnimator.Play();
        }                   
    }
}
