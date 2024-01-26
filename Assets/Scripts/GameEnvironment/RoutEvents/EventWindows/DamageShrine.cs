using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class DamageShrine : RoutEvent
    {
        [SerializeField] private Transform _shrineSlot;
        [SerializeField] private Button _buffButton;
        [SerializeField] private MaterialType _paymentMaterial;
        [SerializeField] private int _paymentAmount;
        [SerializeField] private int _plasticinePaymentAmount;
        [SerializeField] private int _gluePaymentAmount;
        [SerializeField] private int _screwPaymentAmount;
        [SerializeField] private TMP_Text _materialAmount;
        [SerializeField] private TMP_Text _plasticineAmount;
        [SerializeField] private TMP_Text _glueAmount;
        [SerializeField] private TMP_Text _screwAmount;

        protected SkillView _choosedSkill;
        private ConnectingMaterial _currentMaterial;
        private ConnectingMaterial _previousMaterial;

        private void Awake()
        {
            _plasticineAmount.text = _plasticinePaymentAmount.ToString();
            _glueAmount.text = _gluePaymentAmount.ToString();
            _screwAmount.text = _screwPaymentAmount.ToString();
        }

        protected override void Start()
        {
            base.Start();
            _walet.EnableButtons();
            _playerMoney.MaterialChoosed += GetPayment;
            _skillPanel.SkillChoosed += PrepareToBuff;
        }

        private void OnEnable()
        {
            _buffButton.onClick.AddListener(ApplyBuff);
            _close.onClick.AddListener(CloseEvent);
        }

        private void OnDestroy()
        {
            _buffButton.onClick.RemoveListener(ApplyBuff);
            _close.onClick.RemoveListener(CloseEvent);
        }

        protected override void GetPayment(ConnectingMaterial material)
        {
            if (_playerMoney.CurrentMaterialAmount >= GetPaymentValue(_playerMoney.CurrentMaterial))
            {
                if (_currentMaterial == material) return;
                if (_currentMaterial == null)
                {
                    _currentMaterial = material;
                    _playerMoney.RemoveMaterialByType(_currentMaterial.Data.Type, GetPaymentValue(_currentMaterial));
                }
                else
                {
                    _previousMaterial = _currentMaterial;
                    _playerMoney.AddMaterialByType(_previousMaterial.Data.Type, GetPaymentValue(_previousMaterial));
                    _currentMaterial = material;
                    _playerMoney.RemoveMaterialByType(_currentMaterial.Data.Type, GetPaymentValue(_currentMaterial));
                }
            }
            else
                EnableMoneyWarning();
        }

        protected override void PrepareToBuff(SkillView skill)
        {
            if (_currentMaterial == null)
            {
                EnableMaterialWarning();
                return;
            }

            if (_choosedSkill == null)
            {
                _choosedSkill = skill;
                _choosedSkill.transform.SetParent(_shrineSlot.transform);
                _choosedSkill.transform.position = _shrineSlot.position;
                _choosedSkill.SkillButtonPressed -= _skillPanel.ChooseSkill;
            }
            else
            {
                _nextSkill = skill;
                _skillPanel.TakeBack(_choosedSkill);
                _choosedSkill = _nextSkill;
                _choosedSkill.transform.SetParent(_shrineSlot.transform);
                _choosedSkill.transform.position = _shrineSlot.position;
                _choosedSkill.SkillButtonPressed -= _skillPanel.ChooseSkill;
            }
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();
            if (_choosedSkill != null) _skillPanel.TakeBack(_choosedSkill);
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void ApplyBuff()
        {
            if (_choosedSkill == null) return;
            _playerMoney.SaveMoney();

            if (_choosedSkill.SkillData.SkillType == SkillType.Defence)
                _choosedSkill.SkillData.Defence++;
            else
                _choosedSkill.SkillData.Damage++;

            //_choosedSkill.UpdateCooldown();
            _skillPanel.TakeBack(_choosedSkill);
            _shrineSlot.gameObject.SetActive(false);
            _skillPanel.SkillChoosed -= PrepareToBuff;
            _choosedSkill = null;
            Invoke(nameof(CloseEvent), 1f);
        }

        private int GetPaymentValue(ConnectingMaterial material)
        {
            if (material.Data.Type == MaterialType.Plasticine) return _plasticinePaymentAmount;
            else if (material.Data.Type == MaterialType.Glue) return _gluePaymentAmount;
            else if (material.Data.Type == MaterialType.Screw) return _screwPaymentAmount;
            return 0;
        }
    }
}
