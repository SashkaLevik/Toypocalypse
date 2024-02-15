using Assets.Scripts.Data;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.UI;
using Assets.Scripts.GameEnvironment.TreeHouse;

namespace Assets.Scripts.GameEnvironment
{
    public class Garage : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private List<Image> _minionSlots;
        [SerializeField] private Image _altar;
        [SerializeField] private TMP_Text _sacrificePrice;
        [SerializeField] private Button _summonMinion;
        [SerializeField] private Button _exit;
        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _garageWindow;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private ConnectingMaterial _material;
        [SerializeField] private TMP_Text _materialAmount;
        [SerializeField] private PlayerMoney _playerMoney;
        [SerializeField] private Warning _warning;

        private int _minionPrice = 10;
        private int _randomMinion;
        private List<Minion> _minions = new List<Minion>();
        private List<Minion> _activeMinions = new List<Minion>();
        private PlayerProgress _playerProgress;

        private void Start()
        {
            if (_playerProgress.WorldData.IsNewGame == true)
            {
                _minionPrice = 0;
                _sacrificePrice.text = _minionPrice.ToString();
                _summonMinion.onClick.AddListener(()=>_mainMenu.ActivateFirstMinion(_summonMinion));
            }
            else
            {
                _sacrificePrice.text = _minionPrice.ToString();
                _summonMinion.onClick.AddListener(ActivateMinion);
                ShowActiveMinions(_activeMinions);
            }

            _exit.onClick.AddListener(ReturnToMenu);
            UpdateMaterial();
        }       

        private void OnEnable()
        {
                        
        }

        private void OnDestroy()
        {
            _exit.onClick.RemoveListener(ReturnToMenu);
            _summonMinion.onClick.RemoveListener(ActivateMinion);
        }

        public void ShowActiveMinions(List<Minion> minions)
        {
            if (minions.Count > 0)
            {
                for (int i = 0; i < minions.Count; i++)
                {
                    _minionSlots[i].sprite = minions[i].MinionData.Icon;
                }
            }
        }

        private void ActivateMinion()
        {
            if (_playerMoney.GetValue(_material.Data.Type) >= _minionPrice)
            {
                _playerMoney.RemoveMaterialByType(_material.Data.Type, _minionPrice);
                UpdateMaterial();
                _randomMinion = Random.Range(0, _minions.Count);
                _activeMinions.Add(_minions[_randomMinion]);
                _minions.Remove(_minions[_randomMinion]);
                ShowActiveMinions(_activeMinions);
            }
            else
                _warning.Enable(_warning.NoMoney);
        }

        private void UpdateMaterial()
            => _materialAmount.text = _playerMoney.GetValue(_material.Data.Type).ToString();

        private void ReturnToMenu()
        {
            _menuWindow.SetActive(true);
            _garageWindow.SetActive(false);
        }        

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
            _minions = progress.PlayerParts.Minions.ToList();
            _activeMinions = progress.PlayerParts.ActiveMinions.ToList();
        }

        public void Save(PlayerProgress progress)
        {
            if (progress.WorldData.IsNewGame == false)
            {
                progress.PlayerParts.Minions = _minions.ToList();
                progress.PlayerParts.ActiveMinions = _activeMinions.ToList();
            }            
        }
    }
}
