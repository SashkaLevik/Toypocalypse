using Assets.Scripts.Data.StaticData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.GameEnvironment.Dice;

namespace Assets.Scripts.Player
{
    public class Minion : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private Button _minionButton;
        [SerializeField] private SkillData _minionSkillData;
        [SerializeField] private MinionData _minionData;
        [SerializeField] private Image _descriptionBG;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private GameObject _appear;
        [SerializeField] private AudioSource _attackSound;
        [SerializeField] private DiceFace _areaType;

        public Sprite Icon => _icon;
        public MinionData MinionData => _minionData;
        public SkillData MinionSkillData => _minionSkillData;
        public DiceFace AreaType => _areaType;

        public event UnityAction<Minion> MinionButtonPressed;

        private void OnEnable()
            => _minionButton.onClick.AddListener(ChooseMinion);

        public void Appear()
            => Instantiate(_appear);

        public void Activate()
            => _minionButton.interactable = true;

        public void Disactivate()
            => _minionButton.interactable = false;

        public void OnEnter()
        {
            _descriptionBG.gameObject.SetActive(true);
            _description.text = GetLocalizedDescription();
        }

        public void OnExit()
            => _descriptionBG.gameObject.SetActive(false);

        private string GetLocalizedDescription()
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return _minionSkillData.RuDescription;
            else
                return _minionSkillData.EnDescription;
        }        

        private void ChooseMinion()
            => MinionButtonPressed?.Invoke(this);
    }
}
