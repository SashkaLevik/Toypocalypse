using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Artifact;
using Assets.Scripts.Player;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class ArtifactShop : RoutEvent
    {
        [SerializeField] private Image _materialIcon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private List<ArtifactData> _artifactDatas;
        [SerializeField] private List<RectTransform> _slots;
        [SerializeField] private List<BuyButton> _buyButtons;
        [SerializeField] private Artifact _artifactView;

        private Artifact _artifact;
        private Artifact _choosedArtifact;
        private ArtifactsContainer _artifactContainer;

        protected override void Start()
        {
            base.Start();
            InstantiateArtifacts();
            _artifactContainer = _skillPanel.GetComponent<ArtifactsContainer>();

            foreach (var button in _buyButtons)
                button.GetComponent<Button>().onClick.AddListener(() => BuyArtifact(button));
        }

        private void OnEnable()
            => _close.onClick.AddListener(CloseEvent);

        private void OnDestroy()
        {
            _close.onClick.RemoveListener(CloseEvent);

            foreach (var button in _buyButtons)
                button.GetComponent<Button>().onClick.RemoveListener(() => BuyArtifact(button));
        }

        private void BuyArtifact(BuyButton button)
        {
            _choosedArtifact = button.Artifact;

            if (_artifactContainer.CanAddArtifact() == false)
            {
                _warning.Enable(_warning.FullPotions);
                return;
            }

            //if (_playerMoney.GetValue(_choosedArtifact.Data.Material.Data.Type) >= _choosedArtifact.Data.Price)
            //{
            //    _playerMoney.RemoveMaterialByType(_choosedArtifact.Data.Material.Data.Type, _choosedArtifact.Data.Price);
            //    _choosedArtifact.Activate();
            //    _artifactContainer.AddPotion(_choosedArtifact);
            //    button.GetComponent<Button>().interactable = false;
            //}
            //else
            //    _warning.Enable(_warning.NoMoney);
        }

        private void InstantiateArtifacts()
        {
            //int randomPotion = Random.Range(0, _potions.Count);

            for (int i = 0; i < _slots.Count; i++)
            {
                _artifactView.Init(_artifactDatas[i]);
                _artifact = Instantiate(_artifactView, _slots[i]);
                _buyButtons[i].GetArtifact(_artifact);
                _priceText = _slots[i].GetComponentInChildren<TMP_Text>();
                _priceText.text = _artifact.Data.Price.ToString();
                _materialIcon = _slots[i].GetComponentInChildren<Image>();
                _materialIcon.sprite = _artifact.Data.Material.Data.Icon;
                _artifact.InitPlayer(_player);
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
