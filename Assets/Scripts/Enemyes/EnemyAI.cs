using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Enemyes
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private SkillView _skillView;
        [SerializeField] private GameObject _attackPanel;
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private EnemySpeed _enemySpeed;
        [SerializeField] private SkillView _wait;

        private Toy _player;
        private SkillData _skillData;
        private SkillView _currentSkill;
        private int _randomSkill;

        private void Start()
        {
            _player = _enemy.Player;
        }

        public void ChooseAction()
        {
            _randomSkill = Random.Range(0, _enemy.EnemyData.Skills.Count);
            _skillData = _enemy.EnemyData.Skills[_randomSkill];
            _skillView.Init(_skillData);
            
            if (CanAct())
            {
                _enemySpeed.SpentAP(_skillData.RequiredAP);
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

            }
            else
                ApplyAction();
        }        

        private void ApplyAction()
        {
            _player.GetComponent<PlayerHealth>().TakeDamage(_currentSkill.Damage);
            RemoveSkill();
            PrepareSkill();
        }

        private void Wait()
        {
            ApplyAction();
            _currentSkill = Instantiate(_wait, _attackPanel.transform);
            _enemySpeed.RecoverAP();
        }
        
        private void RemoveSkill()
        {
            Destroy(_currentSkill.gameObject);
            _currentSkill = null;
        }

        private bool CanAct()
        {
            if (_skillData.RequiredAP <= _enemySpeed.CurrentSpeed) return true;
            return false;
        }
    }
}
