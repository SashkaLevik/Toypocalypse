using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class StagePrizeCalculator : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private GameObject _materialLootBox;
        [SerializeField] private GameObject _partsLootBox;
        [SerializeField] private BattleSystem _battleSystem;

        private Toy _player;
        private int _boxTypePercent;
        private int _materialPercent = 90;
        private int _changedPercent = 10;
        private int _stageNumber;

        private void Start()
        {           
            _battleSystem.StageCompleted += ChangeBoxPercent;
            _materialPercent -= _stageNumber * 10;
        }

        private void OnDestroy()
            => _battleSystem.StageCompleted -= ChangeBoxPercent;

        private void ChangeBoxPercent()
        {
            _materialPercent -= _changedPercent;
        }              

        public void GetBox()
        {
            _boxTypePercent = Random.Range(1, 100);

            if (_boxTypePercent <= _materialPercent)
                _materialLootBox.SetActive(true);
            else
                _partsLootBox.SetActive(true);
        }

        public void DisableBox()
        {
            _materialLootBox.SetActive(false);
            _partsLootBox.SetActive(false);
        }                

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            _stageNumber = progress.WorldData.Stage;
        }
    }
}
