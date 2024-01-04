using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyAttackPanel : MonoBehaviour
    {
        [SerializeField] private SkillView _skillView;
        [SerializeField] private BaseEnemy _enemy;

        public SkillView _currentSkill;

        private void Start()
        {
            //PrepareSkill();
        }

        public void PrepareSkill(SkillData skillData)
        {
            //_currentSkill = skillView;
            _skillView.Init(skillData);
            _currentSkill = Instantiate(_skillView, transform);
        }

        private SkillData GetSkill()
        {
            int randomSkill = Random.Range(0, _enemy.EnemyData.Skills.Count);
            var data = _enemy.EnemyData.Skills[randomSkill];
            return data;
        }
    }
}
