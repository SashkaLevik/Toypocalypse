using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.GameEnvironment.RoutEvents;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerSpeed))]
    public class Toy : MonoBehaviour, ISaveProgress
    {        
        [SerializeField] protected Sprite _previewImage;
        [SerializeField] private List<PartData> _parts;
        
        private BaseEnemy _enemy;
        private SkillPanel _skillPanel;
        private RoutMap _routMap;
        private PlayerHud _playerHud;
        private List<Minion> _activeMinions;
        private AnimatorController _animator;
        private List<Part> _inactiveParts;
        private RoutEvent _currentEvent;

        public AnimatorController Animator => _animator;
        public List<Part> InactiveParts => _inactiveParts;
        public SkillPanel SkillPanel => _skillPanel;
        public PlayerHud PlayerHud => _playerHud;
        public Sprite PreviewImage => _previewImage;
        public List<PartData> Parts => _parts;
        public List<Minion> Minions => _activeMinions;

        private void Start()
        {
            _skillPanel.LoadPanelOrInitNew();
            _animator = GetComponent<AnimatorController>();
            //_currentArea.AreaType = AreaType.Common;
            _routMap.EventEntered += OnEventEnter;
        }       

        private void OnEventEnter(RoutEvent routEvent)
        {
            _currentEvent = routEvent;
            _currentEvent.EventCompleted += OnEventExit;
        }

        private void OnEventExit()
        {
            _currentEvent.EventCompleted -= OnEventExit;
        }

        public void Construct(SkillPanel skillPanel, RoutMap routMap, PlayerHud playerHud)
        {
            _skillPanel = skillPanel;
            _routMap = routMap;
            _playerHud = playerHud;
        }
        
        public void InitEnemy(BaseEnemy enemy)
            => _enemy = enemy;                                                               

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            _activeMinions = progress.PlayerParts.ActiveMinions;

            if (progress.WorldData.LevelNumber == 1)
                _inactiveParts = progress.PlayerParts.FirstLevelParts.ToList();
            else if (progress.WorldData.LevelNumber == 2)
                _inactiveParts = progress.PlayerParts.SecondLevelParts.ToList();
        }
    }
}
