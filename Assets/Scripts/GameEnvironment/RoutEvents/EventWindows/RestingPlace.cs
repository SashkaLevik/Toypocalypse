using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Potions;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class RestingPlace : RoutEvent
    {
        [SerializeField] private Button _heal;
        [SerializeField] private Button _maxHP;
        [SerializeField] private Button _maxAP;
        [SerializeField] private Button _getPotion;
        [SerializeField] private ConnectingMaterial _paymentMaterial;
        [SerializeField] private int _paymentPrice;
        [SerializeField] private Image _matetialIcon;
        [SerializeField] private TMP_Text _paymentText;
        [SerializeField] private List<PotionData> _potionDatas;
        [SerializeField] private Potion _defaultPotion;
        [SerializeField] private float _healAmount;
        [SerializeField] private List<Button> _eventButtons;
        [SerializeField] private RectTransform _slot;

        private float _healthRiseiseValue = 3;
        private float _speedRiseValue = 2;
        private Potion _potion;
        private PotionContainer _potionContainer;

        protected override void Start()
        {
            base.Start();
            _potionContainer = _skillPanel.GetComponent<PotionContainer>();
            _matetialIcon.sprite = _paymentMaterial.Data.Icon;
            _paymentText.text = _paymentPrice.ToString();
        }

        private void OnEnable()
        {
            _maxHP.onClick.AddListener(RiseHP);
            _maxAP.onClick.AddListener(RiseAP);
            _heal.onClick.AddListener(HealPlayer);
            _getPotion.onClick.AddListener(GetRandomPotion);
            _close.onClick.AddListener(CloseEvent);
        }

        private void OnDestroy()
        {
            _maxHP.onClick.RemoveListener(RiseHP);
            _heal.onClick.RemoveListener(HealPlayer);
            _getPotion.onClick.RemoveListener(GetRandomPotion);
            _close.onClick.RemoveListener(CloseEvent);
        }

        private void RiseAP()
        {
            if (_playerMoney.GetValue(_paymentMaterial.Data.Type) >= _paymentPrice)
            {
                _playerSpeed.MaxSpeed += _speedRiseValue;
                _playerSpeed.CurrentSpeed += _speedRiseValue;
                _playerMoney.RemoveMaterialByType(_paymentMaterial.Data.Type, _paymentPrice);
                OffButtons();
            }
            else
                _warning.Enable(_warning.NoMoney);
        }

        private void RiseHP()
        {
            if (_playerMoney.GetValue(_paymentMaterial.Data.Type) >= _paymentPrice)
            {
                _playerHealth.MaxHP += _healthRiseiseValue;
                _playerHealth.CurrentHP += _healthRiseiseValue;
                _playerMoney.RemoveMaterialByType(_paymentMaterial.Data.Type, _paymentPrice);
                OffButtons();
            }
            else
                _warning.Enable(_warning.NoMoney);
        }

        private void HealPlayer()
        {
            _playerHealth.Heal(_healAmount);
            OffButtons();
        }

        private void GetRandomPotion()
        {
            if (_potionContainer.CanAddPotion() == false)
            {
                _warning.Enable(_warning.FullPotions);
                return;
            }

            int randomPotion = Random.Range(0, _potionDatas.Count);
            _defaultPotion.Init(_potionDatas[randomPotion]);
            _potion = Instantiate(_defaultPotion, _slot);
            _potion.Activate();
            _potionContainer.AddPotion(_potion);
            _potion.InitPlayer(_player);
            OffButtons();
        }

        private void OffButtons()
        {
            foreach (var button in _eventButtons)
                button.interactable = false;
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
