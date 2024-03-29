using Assets.Scripts.GameEnvironment.TreeHouse;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class CurrencyExchenge : RoutEvent
    {
        [SerializeField] private Sprite _defaultImage;
        [SerializeField] private Image _materialIcon;
        [SerializeField] private ConnectingMaterial _plasticine;
        [SerializeField] private ConnectingMaterial _glue;
        [SerializeField] private ConnectingMaterial _screw;
        [SerializeField] private List<RectTransform> _convertingSlots;
       
        private ConnectingMaterial _currentMaterial;
        private ConnectingMaterial _previousMaterial;
        private ConnectingMaterial _convertingMaterial1;
        private ConnectingMaterial _convertingMaterial2;

        protected override void Start()
        {
            base.Start();
            _playerMoney.MaterialChoosed += GetPayment;
        }

        private void OnEnable()
            => _close.onClick.AddListener(CloseEvent);       

        protected override void GetPayment(ConnectingMaterial material)
        {
            if (_playerMoney.CurrentMaterialAmount >= GetPaymentValue(_playerMoney.CurrentMaterial))
            {
                if (_currentMaterial == material) return;
                if (_currentMaterial == null)
                {
                    _currentMaterial = material;
                    ChangeIcon(_currentMaterial);
                    GetConvertingMaterials();
                }
                else
                {
                    _previousMaterial = _currentMaterial;
                    _currentMaterial = material;
                    ChangeIcon(_currentMaterial);
                    RemoveMaterials();
                    GetConvertingMaterials();
                }
            }
            else
                _warning.Enable(_warning.NoMoney);
        }

        private void GetConvertingMaterials()
        {
            if (_currentMaterial.Data.Type == MaterialType.Plasticine)
                SetConvertingMaterials(_glue, _screw);
            else if (_currentMaterial.Data.Type == MaterialType.Glue)
                SetConvertingMaterials(_plasticine, _screw);
            else if (_currentMaterial.Data.Type == MaterialType.Screw)
                SetConvertingMaterials(_plasticine, _glue);
        }

        private void SetConvertingMaterials(ConnectingMaterial material1, ConnectingMaterial material2)
        {
            _convertingMaterial1 = Instantiate(material1, _convertingSlots[0]);
            _convertingMaterial2 = Instantiate(material2, _convertingSlots[1]);
            _convertingMaterial1.MaterialChoosed += Convert;
            _convertingMaterial2.MaterialChoosed += Convert;
        }

        private void Convert(ConnectingMaterial material)
        {
            _playerMoney.AddMaterialByType(material.Data.Type, GetPaymentValue(material));
            _playerMoney.RemoveMaterialByType(_currentMaterial.Data.Type, GetPaymentValue(_currentMaterial));
            _currentMaterial = null;
            _materialIcon.gameObject.SetActive(false);
            _convertingMaterial1.MaterialChoosed -= Convert;
            _convertingMaterial2.MaterialChoosed -= Convert;
            RemoveMaterials();
        }

        private void RemoveMaterials()
        {
            foreach (var slot in _convertingSlots)
            {
                if (slot.GetComponentInChildren<ConnectingMaterial>() != null)
                    Destroy(slot.GetComponentInChildren<ConnectingMaterial>().gameObject);
            }
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();
            _playerMoney.MaterialChoosed -= GetPayment;
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void ChangeIcon(ConnectingMaterial material)
        {
            _materialIcon.gameObject.SetActive(true);
            _materialIcon.sprite = material.Data.Icon;
        }
    }
}
