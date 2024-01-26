using Assets.Scripts.Data.StaticData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class Minion : MonoBehaviour
    {
        [SerializeField] private SkillView _uiSkillView;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Button _minionButton;
        [SerializeField] private SkillData _minionData;
        [SerializeField] private bool _isAvalable;

        public SkillData MinionData => _minionData;

        public bool IsAvalable => _isAvalable;

        public event UnityAction<Minion> MinionButtonPressed;

        private void Start()
        {
            _uiSkillView.Init(_minionData);
        }

        private void OnEnable()
            => _minionButton.onClick.AddListener(ChooseMinion);

        private void ChooseMinion()
            => MinionButtonPressed?.Invoke(this);
    }
}
