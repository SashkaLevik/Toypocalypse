using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class PartsLootBox : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private Button _openBox;
        [SerializeField] private Image _newPartImage;
        [SerializeField] private BoxAnimator _boxAnimator;
        [SerializeField] private BattleSystem _battleSystem;

        private List<Part> _inactiveParts = new();
        private List<Part> _activeParts = new();
        private Part _currentPart;
        private int _randomPart;

        private void OnEnable()
        {
            _openBox.onClick.AddListener(ActivateNewPart);
            _battleSystem.StageCompleted += CloseBox;
        }

        private void OnDestroy()
        {
            _openBox.onClick.RemoveListener(ActivateNewPart);
            _battleSystem.StageCompleted -= CloseBox;
        }

        private void ActivateNewPart()
        {
            _boxAnimator.OpenBox();

            if (_inactiveParts.Count > 0)
            {
                _randomPart = Random.Range(0, _inactiveParts.Count);
                _currentPart = _inactiveParts[0];
                _newPartImage.gameObject.SetActive(true);
                _newPartImage.sprite = _currentPart.PartData.Icon;
                //_currentPart = _inactiveParts[_randomPart];
            }

            _openBox.interactable = false;
            SaveActivatedPart();            
        }

        private void SaveActivatedPart()
        {
            _inactiveParts.Remove(_currentPart);
            _activeParts.Add(_currentPart);
        }

        private void CloseBox()
        {
            _openBox.interactable = true;
            _newPartImage.gameObject.SetActive(false);
        }

        public void Load(PlayerProgress progress)
        {
            _activeParts = progress.PlayerParts.ActiveParts.ToList();

            if (progress.WorldData.LevelNumber == 1)
                _inactiveParts = progress.PlayerParts.FirstLevelParts.ToList();
            else if (progress.WorldData.LevelNumber == 2)
                _inactiveParts = progress.PlayerParts.SecondLevelParts.ToList();
        }

        public void Save(PlayerProgress progress)
        {
            if (progress.WorldData.LevelNumber == 1)
                progress.PlayerParts.FirstLevelParts = _inactiveParts.ToList();
            else if (progress.WorldData.LevelNumber == 2)
                progress.PlayerParts.SecondLevelParts = _inactiveParts.ToList();

            //progress.PlayerParts.InactiveParts = _inactiveParts.ToList();
            progress.PlayerParts.ActiveParts = _activeParts.ToList();
        }
    }
}
