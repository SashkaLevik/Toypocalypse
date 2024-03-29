using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BattleValuesView : MonoBehaviour
    {
        [SerializeField] private SkillPanel _skillPanel;
        
        private Toy _player;
        private Area _currentArea;
        private PlayerHud _playerHud;

        private void Start()
        {
            _player = _skillPanel.Player;
            _playerHud = _skillPanel.PlayerHud;
        }

        public void ViewOnLeftButton()
        {
            _currentArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.GetComponent<SkillAreaWatcher>().ShowOnLeftButton(_currentArea);
        }

        public void ViewOnRightButton()
        {
            _currentArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.GetComponent<SkillAreaWatcher>().ShowOnRightButton(_currentArea);
        }

        public void SetDefaultLeft()
        {
            _currentArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.GetComponent<SkillAreaWatcher>().SetDefaultLeft(_currentArea);
        }

        public void SetDefaultRight()
        {
            _currentArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.GetComponent<SkillAreaWatcher>().SetDefaultRight(_currentArea);
        }
    }
}
