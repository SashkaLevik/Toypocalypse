using Assets.Scripts.Data.StaticData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
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

        public Sprite Icon => _icon;
        public MinionData MinionData => _minionData;
        public SkillData MinionSkillData => _minionSkillData;

        public event UnityAction<Minion> MinionButtonPressed;

        public void Appear()
            => Instantiate(_appear); 

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

        private void OnEnable()
            => _minionButton.onClick.AddListener(ChooseMinion);

        private void ChooseMinion()
            => MinionButtonPressed?.Invoke(this);
    }
}
