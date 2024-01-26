using Assets.Scripts.Data.StaticData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _AP;
        [SerializeField] private Image _skillIcon;       
        [SerializeField] private Image[] _fullImages;
        [SerializeField] private Image[] _emptyImages;
        [SerializeField] private Button _skillButton;
        [SerializeField] private GameObject _imageContainer;
        [SerializeField] private SkillPanel _skillPanel;

        public float _cooldown;
        public float _damage;
        public float _requiredAP;
        public float _turnsToRecover;
        public SkillData _skillData;

        public float Damage => _damage;
        public float RequiredAP => _requiredAP;
        public SkillData SkillData => _skillData;
        public Button SkillButton => _skillButton;

        public event UnityAction<SkillView> SkillButtonPressed;

        private void Start()
            => _skillPanel = GetComponentInParent<SkillPanel>();

        private void OnEnable()
            => _skillButton.onClick.AddListener(OnButtonPress);

        private void OnDestroy()
            => _skillButton.onClick.RemoveListener(OnButtonPress);        

        public void Init(SkillData skillData)
        {
            _skillData = skillData;

            if (_skillData.SkillType == SkillType.Defence)
            {
                _damage = _skillData.Defence;
                SetParameters(_skillData);                
            }
            else if (skillData.SkillType == SkillType.Move)
            {
                _damage = 0;
                SetParameters(_skillData);                
            }
            else
            {
                _damage = _skillData.Damage;
                SetParameters(_skillData);
            }

            UpdateCooldown();                                             
        }        

        public void ApplyCooldown()
        {
            for (int i = 0; i < SkillData.Cooldown; i++)
            {
                _fullImages[i].gameObject.SetActive(false);
                _emptyImages[i].gameObject.SetActive(true);
            }
                
            SkillButton.interactable = false;
        }

        public void RecoverCooldown()
        {
            _turnsToRecover+=1;
            
            if (_turnsToRecover <= SkillData.Cooldown)
            {
                for (int i = 0; i < _turnsToRecover; i++)
                {
                    _fullImages[i].gameObject.SetActive(true);
                    _emptyImages[i].gameObject.SetActive(false);
                }                    

                if (_turnsToRecover == SkillData.Cooldown)
                {
                    SkillButton.interactable = true;
                    _turnsToRecover = 0;
                    _skillPanel.SkillPlayed -= RecoverCooldown;
                }
            }
        }

        public void ResetCooldown()
        {
            SkillButton.interactable = true;
            _turnsToRecover = 0;
            UpdateCooldown();
        }

        public void UpdateCooldown()
        {
            for (int i = 0; i < _emptyImages.Length; i++)
                _emptyImages[i].gameObject.SetActive(false);

            for (int i = 0; i < _fullImages.Length; i++)
                _fullImages[i].gameObject.SetActive(false);

            for (int i = 0; i < SkillData.Cooldown; i++)
                _fullImages[i].gameObject.SetActive(true);
        }

        private void SetParameters(SkillData skillData)
        {
            _damageText.text = _damage.ToString();
            _cooldown = skillData.Cooldown;
            _requiredAP = skillData.RequiredAP;
            _AP.text = _requiredAP.ToString();
            _skillIcon.sprite = skillData.Icon;
        }

        private void OnButtonPress()
            => SkillButtonPressed?.Invoke(this);
    }
}
