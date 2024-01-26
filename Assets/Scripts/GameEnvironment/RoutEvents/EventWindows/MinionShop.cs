using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class MinionShop : RoutEvent
    {
        private const string Minions = "Minions";

        [SerializeField] private List<SkillData> _skillDatas;
        [SerializeField] private List<Image> _minionsContainer;
        [SerializeField] private Image _preview;
        [SerializeField] private Button _takeMinion;

        private List<Minion> _allMinions;
        private List<Minion> _minions = new List<Minion>();
        private Minion _currentMinion;
        private Minion _choosedMinion;

        private void Awake()
        {
            _allMinions = Resources.LoadAll<Minion>(Minions).ToList();
            GetAvalableMinions();
            InstantiateMinions();
        }

        private void OnEnable()
        {
            _takeMinion.onClick.AddListener(TakeMinion);
            _close.onClick.AddListener(CloseEvent);
        }

        private void OnDestroy()
        {
            _takeMinion.onClick.RemoveListener(TakeMinion);
            _close.onClick.RemoveListener(CloseEvent);
        }

        private void GetAvalableMinions()
        {
            foreach (var minion in _allMinions)
            {
                if (minion.IsAvalable == true)
                {
                    _minions.Add(minion);
                }
            }
        }

        private void InstantiateMinions()
        {
            for (int i = 0; i < _minions.Count; i++)
            {
                _currentMinion = Instantiate(_minions[i], _minionsContainer[i].transform);
                _currentMinion.MinionButtonPressed += ChooseMinion;
            }
        }

        private void ChooseMinion(Minion minion)
        {
            _choosedMinion = minion;
            _preview.sprite = _choosedMinion.MinionData.Icon;
        }

        private void TakeMinion()
        {
            _skillPanel.AddMinion(_choosedMinion.MinionData);
            _takeMinion.interactable = false;
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
