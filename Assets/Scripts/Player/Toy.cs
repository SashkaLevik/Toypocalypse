﻿using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerSpeed))]
    public class Toy : MonoBehaviour, ISaveProgress
    {
        [SerializeField] protected Sprite _toyImage;
        [SerializeField] private List<PartData> _parts;
        [SerializeField] private Canvas _attackPanel;
        
        private float _health;
        private float _speed;
        private BaseEnemy _enemy;
        private SkillPanel _skillPanel;
        private RoutMap _routMap;
        private PlayerHud _playerHud;
        private PlayerProgress _playerProgress;
        private List<Minion> _activeMinions;

        public float Health => _health;
        public float Speed => _speed;
        public SkillPanel SkillPanel => _skillPanel;
        public PlayerHud PlayerHud => _playerHud;
        public Sprite ToyImage => _toyImage;
        public PlayerProgress Progress => _playerProgress;
        public List<PartData> Parts => _parts;
        public List<Minion> Minions => _activeMinions;
        public BaseEnemy Enemy => _enemy;

        public event UnityAction<AreaType> AreaChanged;
        

        private void Start()
        {
            _attackPanel.worldCamera = Camera.main;
            _skillPanel.LoadPanelOrInitNew();            
        }
        
        public void Construct(SkillPanel skillPanel, RoutMap routMap, PlayerHud playerHud)
        {
            _skillPanel = skillPanel;
            _routMap = routMap;
            _playerHud = playerHud;
        }            

        public void InitEnemy(BaseEnemy enemy)
            => _enemy = enemy;

        public void SetParameters(float health, float speed)
        {
            _health = health;
            _speed = speed;
        }        

        public void ChangeArea(AreaType type)
            => AreaChanged?.Invoke(type);        

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            _activeMinions = progress.PlayerParts.ActiveMinions;
        }
    }
}
