using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.States;
using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameManagment
{
    public class LevelLoader : MonoBehaviour, ISaveProgress
    {       
        [SerializeField] private TreeHouseUI _treeHouse;
        [SerializeField] private Map _map;
        
        private string _level;

        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnEnable()
        {
            _map.LevelLoaded += OnLevelLoad;
        }

        private void OnLevelLoad(string level)
        {
            _level = level;
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<LevelState, string>(level, _treeHouse.ToyData);
        }

        public void Save(PlayerProgress progress)
        {            
        }

        public void Load(PlayerProgress progress)
        {            
        }
    }
}
