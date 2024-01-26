using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Enemyes
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private SkillView _skillView;
        [SerializeField] private GameObject _attackPanel;
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private SkillView _wait;

        public AreaType _areaType;
        private Toy _player;
        private SkillData _skillData;
        private SkillView _currentSkill;
        private SkillView _waitSkill;
        private EnemyMovement _movement;
        private EnemySpeed _enemySpeed;
        private int _randomSkill;        

        private void Start()
        {
            _player = _enemy.Player;
            _movement = GetComponent<EnemyMovement>();
            _enemySpeed = GetComponent<EnemySpeed>();
            _enemy.AreaChanged += OnAreaChanged;
        }        

        public void ChooseAction()
        {
            _randomSkill = Random.Range(0, _enemy.EnemyData.Skills.Count);
            _skillData = _enemy.EnemyData.Skills[_randomSkill];
            _skillView.Init(_skillData);
            if (_waitSkill != null)
            {
                Destroy(_waitSkill.gameObject);
                _waitSkill = null;
            }
            if (CanAct())
            {                
                PrepareSkill();
            }
            else
                Wait();
        }

        private void PrepareSkill()
        {
            if (_currentSkill == null)
            {
                _currentSkill = Instantiate(_skillView, _attackPanel.transform);
                _enemySpeed.SpentAP(_skillData.RequiredAP);
            }
            else
                ApplyAction();
        }        

        private void ApplyAction()
        {
            if (CanApply())
            {
                if (_currentSkill.SkillData.SkillType == SkillType.Defence)
                    _enemy.GetComponent<EnemyHealth>().IncreaseDefence(_currentSkill.SkillData.Defence);
                else
                {
                    _player.GetComponent<PlayerHealth>().TakeDamage(_currentSkill.Damage);
                }
                RemoveSkill();
            }
            else
                ChangeArea();
        }        

        private void Wait()
        {
            ApplyAction();
            _waitSkill = Instantiate(_wait, _attackPanel.transform);
            _enemySpeed.RecoverAP();
        }
        
        private void RemoveSkill()
        {
            Destroy(_currentSkill.gameObject);
            _currentSkill = null;
        }

        private void OnAreaChanged(AreaType type)
            => _areaType = type;

        private bool CanAct()
        {
            if (_skillData.RequiredAP <= _enemySpeed.CurrentSpeed) return true;
            return false;
        }

        private bool CanApply()
        {            
            if (_currentSkill.SkillData.SkillType == SkillType.Defence)
                return true;
            else if (_currentSkill.SkillData.SkillType == SkillType.Melee && _areaType == AreaType.Melee)
                return true;
            else if (_currentSkill.SkillData.SkillType == SkillType.Range && _areaType == AreaType.Range)
                return true;

            return false;
        }
        
        private void ChangeArea()
        {
            if (CanAct() == false) return;

            if (_currentSkill.SkillData.SkillType == SkillType.Melee)
                _movement.MoveLeft();
            else if (_currentSkill.SkillData.SkillType == SkillType.Range)
                _movement.MoveRight();
        }
    }
}
