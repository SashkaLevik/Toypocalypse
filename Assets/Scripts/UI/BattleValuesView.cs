using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BattleValuesView : MonoBehaviour
    {
        [SerializeField] private SkillPanel _skillPanel;
        
        private Toy _player;
        private AreaType _showedArea;
        private PlayerHud _playerHud;

        private void Start()
        {
            _player = _skillPanel.Player;
            _playerHud = _skillPanel.PlayerHud;
        }

        public void ViewOnLeftButton()
        {
            _showedArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.ShowOnLeftButton(_showedArea);

            _playerHud.ShowOnLeft(_showedArea);
        }

        public void ViewOnRightButton()
        {
            _showedArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.ShowOnRightButton(_showedArea);

            _playerHud.ShowOnRight(_showedArea);
        }

        public void SetDefaultLeft()
        {
            _showedArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.SetDefaultLeft(_showedArea);

            _playerHud.SetDefault();
        }

        public void SetDefaultRight()
        {
            _showedArea = _player.CurrentArea;

            foreach (var skill in _skillPanel.PlayerSkills)
                skill.SetDefaultRight(_showedArea);

            _playerHud.SetDefault();
        }
    }
}
