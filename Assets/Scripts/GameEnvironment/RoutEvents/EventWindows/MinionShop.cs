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
        [SerializeField] private List<SkillData> _skillDatas;
        [SerializeField] private List<RectTransform> _minionsContainer;
        [SerializeField] private Image _preview;
        [SerializeField] private Button _takeMinion;

        private List<Minion> _minions = new List<Minion>();
        private Minion _currentMinion;
        private Minion _choosedMinion;       

        protected override void Start()
        {
            base.Start();
            _minions = _player.Minions.ToList();
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

        private void InstantiateMinions()
        {
            if (_minions.Count > 0)
            {
                for (int i = 0; i < _minions.Count; i++)
                {
                    _currentMinion = Instantiate(_minions[i], _minionsContainer[i].transform);
                    _currentMinion.MinionButtonPressed += ChooseMinion;
                }
            }            
        }

        private void ChooseMinion(Minion minion)
        {
            _choosedMinion = minion;
            _preview.gameObject.SetActive(true);
            _takeMinion.gameObject.SetActive(true);
            _preview.sprite = _choosedMinion.Icon;
        }

        private void TakeMinion()
        {
            _skillPanel.AddMinion(_choosedMinion);
            _preview.gameObject.SetActive(false);
            _takeMinion.gameObject.SetActive(false);
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
