using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameManagment
{
    public class LevelLoader : MonoBehaviour
    {
        public const string Home = "Home";

        [SerializeField] private TreeHouseUI _treeHouse;
        [SerializeField] private Map _map;

        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnEnable()
        {
            _map.HomeLoaded += OnHomeLoad;
        }

        private void OnHomeLoad()
        {
            _stateMachine.Enter<BattleState, string>(Home, _treeHouse.ToyData);
        }
    }
}
