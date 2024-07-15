using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenuTutorial : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private MaterialsPanel _materialsPanel;
        [SerializeField] private Table _table;
        [SerializeField] private Image _garageExplain;
        [SerializeField] private Image _minionExplain;
        [SerializeField] private Image _minionActivated;
        [SerializeField] private Image _treeHouseExplain;
        [SerializeField] private Image _containerExplain;
        [SerializeField] private Image _materialExplain;
        [SerializeField] private Image _constructExplain;
        [SerializeField] private Button _garage;
        [SerializeField] private Button _getMinion;
        [SerializeField] private Button _exitGarage;
        [SerializeField] private Button _treeHouse;
        [SerializeField] private Button _material;
        [SerializeField] private Button _construct;

        private PlayerProgress _playerProgress;

        private void Start()
        {
            if (_playerProgress.WorldData.IsNewGame == true)
            {
                _treeHouse.interactable = false;
                _garageExplain.gameObject.SetActive(true);
                _garage.onClick.AddListener(OnGarageClick);
                _getMinion.onClick.AddListener(OnMinionGet);
                _exitGarage.onClick.AddListener(OnExitGarage);
                _treeHouse.onClick.AddListener(OnTreeEnter);
                _materialsPanel.PanelEnabled += PutOnTable;
                _material.onClick.AddListener(OnMaterial);
                _construct.onClick.AddListener(OnConstruct);
            }            
        }        

        private void OnDestroy()
        {
            _garage.onClick.RemoveListener(OnGarageClick);
            _getMinion.onClick.RemoveListener(OnMinionGet);
            _exitGarage.onClick.RemoveListener(OnExitGarage);
            _treeHouse.onClick.RemoveListener(OnTreeEnter);
            _materialsPanel.PanelEnabled -= PutOnTable;
            _material.onClick.RemoveListener(OnMaterial);
            _construct.onClick.RemoveListener(OnConstruct);
        }

        private void OnGarageClick()
        {
            ShowNext(_garageExplain, _minionExplain);
            _exitGarage.interactable = false;
        }

        private void OnMinionGet()
        {
            ShowNext(_minionExplain, _minionActivated);
            _exitGarage.interactable = true;
            _treeHouse.interactable = true;
            _treeHouse.GetComponent<MenuSignboard>().DetectLanguage();
        }        

        private void OnExitGarage()
        {
            ShowNext(_minionActivated, _treeHouseExplain);            
        }

        private void OnTreeEnter()
            => ShowNext(_treeHouseExplain, _containerExplain);

        private void PutOnTable()
        {
            ShowNext(_containerExplain, _materialExplain);
        }

        private void OnMaterial()
        {
            ShowNext(_materialExplain, _constructExplain);
            _table.DisableOnTutorial();
        }            

        private void OnConstruct()
        {
            gameObject.SetActive(false);
        }            

        private void ShowNext(Image current, Image next)
        {
            current.gameObject.SetActive(false);
            next.gameObject.SetActive(true);
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }

        public void Save(PlayerProgress progress)
        {
        }        
    }
}
