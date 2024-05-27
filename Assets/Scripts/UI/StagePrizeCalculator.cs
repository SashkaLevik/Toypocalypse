using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StagePrizeCalculator : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private GameObject _materialLootBox;
        [SerializeField] private GameObject _partsLootBox;
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private PartsLootBox _partsBox;

        private Toy _player;
        private int _boxTypePercent;
        private int _materialPercent = 90;
        private int _changedPercent = 10;
        private int _stageNumber;
        private int _inactiveParts;
        private PlayerProgress _playerProgress;
        //private int _bossStage = 5;

        private void Start()
        {           
            _battleSystem.StageCompleted += ChangeBoxPercent;
            _materialPercent -= _stageNumber * 10;

            //if (_stageNumber == _bossStage)
            //    _materialPercent = 0;
            //else
            //    _materialPercent -= _stageNumber * 10;
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

            if (_boxTypePercent >= _materialPercent && _partsBox.InactiveParts.Count > 0)
                _partsLootBox.SetActive(true);
            else
                _materialLootBox.SetActive(true);
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
            _playerProgress = progress;
        }
    }
}
