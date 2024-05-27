using Assets.Scripts.GameEnvironment.Items.Potions;
using Assets.Scripts.GameEnvironment.TreeHouse;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Player;
using Assets.Scripts.Data.StaticData;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class PotionShop : RoutEvent
    {
        [SerializeField] private Image _materialIcon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private List<PotionData> _potionDatas; 
        [SerializeField] private List<RectTransform> _slots;
        [SerializeField] private List<BuyButton> _buyButtons;
        [SerializeField] private List<Image> _materialImages;
       
        private PotionData _choosedPotion;
        private PotionContainer _potionContainer;

        protected override void Start()
        {
            base.Start();
            InstantiatePotions();
            _potionContainer = _skillPanel.GetComponent<PotionContainer>();

            foreach (var button in _buyButtons)
                button.GetComponent<Button>().onClick.AddListener(() => BuyPotion(button));
        }

        private void OnEnable()
            => _close.onClick.AddListener(CloseEvent);              

        private void BuyPotion(BuyButton button)
        {
            _choosedPotion = button.Potion;

            if (_potionContainer.CanAddPotion() == false)
            {
                _warning.Enable(_warning.FullSlots);
                return;
            }

            if (_playerMoney.GetValue(_choosedPotion.Material.Data.Type) >= _choosedPotion.Price)
            {
                _playerMoney.RemoveMaterialByType(_choosedPotion.Material.Data.Type, _choosedPotion.Price);
                _potionContainer.AddPotion(_choosedPotion);
                button.GetComponent<Button>().interactable = false;
            }
            else
                _warning.Enable(_warning.NoMoney);
        }

        private void InstantiatePotions()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                Instantiate(_potionDatas[i].Prefab, _slots[i]);
                _buyButtons[i].GetPotion(_potionDatas[i]);
                _priceText = _buyButtons[i].GetComponentInChildren<TMP_Text>();
                _priceText.text = _potionDatas[i].Price.ToString();
                _materialIcon = _materialImages[i];
                _materialIcon.sprite = _potionDatas[i].Material.Data.Icon;                
            }
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();

            foreach (var button in _buyButtons)
                button.GetComponent<Button>().onClick.RemoveListener(() => BuyPotion(button));

            _close.onClick.RemoveListener(CloseEvent);
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
