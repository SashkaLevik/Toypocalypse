using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Dice;
using Assets.Scripts.Player;
using Lean.Localization;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PartDescription : MonoBehaviour
    {
        private const string Russian = "Russian";

        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private TMP_Text _partName;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _material;
        [SerializeField] private SkillView _skillView;
        [SerializeField] private TMP_Text _skillDescription;
        [SerializeField] private Image _cooldownDescription;
        [SerializeField] private Image _apDescription;
        [SerializeField] private Image _skillDescriptionImage;
        [SerializeField] private Image _layoutImage;
        [SerializeField] private DiceLayout _diceLayout;

        private PartData _choosedPart;
        public PartData ChoosedPart => _choosedPart;

        public void OnEnter()
        {
            if (_choosedPart.Type == PartType.Legs)
            {
                _layoutImage.gameObject.SetActive(true);
                _diceLayout.InitDice(_choosedPart.SkillData.AreaDice);                
            }
            _cooldownDescription.gameObject.SetActive(true);
            _apDescription.gameObject.SetActive(true);
            _skillDescriptionImage.gameObject.SetActive(true);
        }

        public void OnExit()
        {
            _cooldownDescription.gameObject.SetActive(false);
            _apDescription.gameObject.SetActive(false);
            _skillDescriptionImage.gameObject.SetActive(false);
            _layoutImage.gameObject.SetActive(false);
        }

        public void SetValues(PartData partData)
        {
            _choosedPart = partData;
            _partName.text = GetLocalizedName(partData);
            _health.text = partData.Health.ToString();
            _speed.text = partData.Speed.ToString();
            _material.text = partData.MaterialAmount.ToString();
            _skillDescription.text = GetLocalizedSkill(partData.SkillData);
            _skillView.Init(partData.SkillData);           
        }

        private string GetLocalizedName(PartData partData)
        {
            if (_leanLocalization.CurrentLanguage == Russian)
                return partData.NameRu;
            else
                return partData.NameEn;
        }

        private string GetLocalizedSkill(SkillData skillData)
        {
            if (_leanLocalization.CurrentLanguage == Russian)
                return skillData.RuDescription;
            else
                return skillData.EnDescription;
        }
    }
}
