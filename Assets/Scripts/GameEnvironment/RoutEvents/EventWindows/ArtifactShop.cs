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
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private List<ArtifactData> _artifactDatas;
        [SerializeField] private List<RectTransform> _slots;
        [SerializeField] private List<BuyButton> _buyButtons;
        [SerializeField] private List<Image> _materialImages;

        private Artifact _artifact;
        private ArtifactData _choosedArtifact;
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

        private void InstantiateArtifacts()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                Instantiate(_artifactDatas[i].Prefab, _slots[i]);
                _buyButtons[i].GetArtifact(_artifactDatas[i]);
                _priceText = _buyButtons[i].GetComponentInChildren<TMP_Text>();
                _priceText.text = _artifactDatas[i].Price.ToString();
                _materialImages[i].sprite = _artifactDatas[i].Material.Data.Icon;
            }
        }

        private void BuyArtifact(BuyButton button)
        {
            _choosedArtifact = button.Artifact;

            if (_artifactContainer.CanAddArtifact() == false)
            {
                _warning.Enable(_warning.FullSlots);
                Debug.Log("Cant Add");
                return;
            }

            if (_playerMoney.GetValue(_choosedArtifact.Material.Data.Type) >= _choosedArtifact.Price)
            {
                _playerMoney.RemoveMaterialByType(_choosedArtifact.Material.Data.Type, _choosedArtifact.Price);
                _artifactContainer.AddArtifact(_choosedArtifact);
                button.GetComponent<Button>().interactable = false;
            }
            else
                _warning.Enable(_warning.NoMoney);
        }        

        protected override void CloseEvent()
        {
            base.CloseEvent();
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
