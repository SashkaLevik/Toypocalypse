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
        [SerializeField] private Potion _defaultPotion;
       
        private Potion _potion;
        private Potion _choosedPotion;
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

        private void OnDestroy()
        {
            _close.onClick.RemoveListener(CloseEvent);

            foreach (var button in _buyButtons)
                button.GetComponent<Button>().onClick.RemoveListener(() => BuyPotion(button));
        }        

        private void BuyPotion(BuyButton button)
        {
            _choosedPotion = button.Potion;

            if (_potionContainer.CanAddPotion() == false)
            {
                _warning.Enable(_warning.FullPotions);
                return;
            }

            if (_playerMoney.GetValue(_choosedPotion.Data.Material.Data.Type) >= _choosedPotion.Data.Price)
            {
                _playerMoney.RemoveMaterialByType(_choosedPotion.Data.Material.Data.Type, _choosedPotion.Data.Price);
                _choosedPotion.Activate();
                _potionContainer.AddPotion(_choosedPotion);
                button.GetComponent<Button>().interactable = false;
            }
            else
                _warning.Enable(_warning.NoMoney);
        }

        private void InstantiatePotions()
        {
            //int randomPotion = Random.Range(0, _potions.Count);

            for (int i = 0; i < _slots.Count; i++)
            {
                _defaultPotion.Init(_potionDatas[i]);
                _potion = Instantiate(_defaultPotion, _slots[i]);
                _buyButtons[i].GetPotion(_potion);
                _priceText = _slots[i].GetComponentInChildren<TMP_Text>();
                _priceText.text = _potion.Data.Price.ToString();
                _materialIcon = _materialImages[i];
                _materialIcon.sprite = _potion.Data.Material.Data.Icon;
                _potion.InitPlayer(_player);
            }
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
