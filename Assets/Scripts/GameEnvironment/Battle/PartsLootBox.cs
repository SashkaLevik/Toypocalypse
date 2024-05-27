using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class PartsLootBox : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private Button _openBox;
        [SerializeField] private BoxAnimator _boxAnimator;
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private RectTransform _partPosition;
        [SerializeField] private RectTransform _firstMove;
        [SerializeField] private RectTransform _secondMove;

        private float _speed = 1500f;
        private List<Part> _inactiveParts = new List<Part>();
        private List<Part> _activeParts = new List<Part>();
        private Part _currentPart;
        private Part _activatedPart;
        private Vector3 _startPos;

        private void Start()
        {
            _startPos = _partPosition.position;
        }

        public List<Part> InactiveParts => _inactiveParts;

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
                _currentPart = _inactiveParts[0];
                _activatedPart = Instantiate(_currentPart, _partPosition.transform);
                StartCoroutine(MovePart(_partPosition.gameObject));              
                SaveActivatedPart();
            }

            _battleSystem.OnLootBoxOpened();
            _openBox.interactable = false;                        
        }

        private IEnumerator MovePart(GameObject part)
        {
            yield return null;
            yield return new WaitForSeconds(0.1f);

            while (part.transform.position != _firstMove.transform.position)
            {
                part.transform.position = Vector3.MoveTowards(part.transform.position, _firstMove.position, Time.deltaTime * _speed);
                yield return null;
            }
            while (part.transform.position != _secondMove.transform.position)
            {
                part.transform.position = Vector3.MoveTowards(part.transform.position, _secondMove.position, Time.deltaTime * _speed);
                yield return null;
            }
        }

        private void SaveActivatedPart()
        {
            _inactiveParts.Remove(_currentPart);
            _activeParts.Add(_currentPart);
        }

        private void CloseBox()
        {
            if(_activatedPart != null)
                Destroy(_activatedPart.gameObject);

            _partPosition.transform.position = _startPos;
            _openBox.interactable = true;
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
