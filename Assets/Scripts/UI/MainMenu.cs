﻿using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.States;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour, ISaveProgress
    {
        private const string DefaultParts = "Player/DefaultParts";
        private const string FirstLevelParts = "Player/1LevelParts";
        private const string SecondLevelParts = "Player/2LevelParts";
        private const string ThirdLevelParts = "Player/3LevelParts";
        private const string Minions = "Minions";

        [SerializeField] private Button _newRun;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _garageButton;
        [SerializeField] private Button _quit;
        [SerializeField] private GameObject _treeHouseWindow;
        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _garageWindow;
        [SerializeField] private GameObject _warningWindow;
        [SerializeField] private GameObject _settingsWindow;
        [SerializeField] private Garage _garage;
        [SerializeField] private AudioSource _menuTheme;
        [SerializeField] private AudioSource _treeHouseTheme;

        private List<Part> _firstLevelParts;
        private List<Part> _secondLevelParts;
        private List<Part> _thirdLevelParts;
        private List<Minion> _minions = new List<Minion>();
        private List<Minion> _activeMinions = new List<Minion>();
        private PlayerProgress _playerProgress;
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _firstLevelParts = Resources.LoadAll<Part>(FirstLevelParts).ToList();
            _secondLevelParts = Resources.LoadAll<Part>(SecondLevelParts).ToList();
            _thirdLevelParts = Resources.LoadAll<Part>(ThirdLevelParts).ToList();
            _minions = Resources.LoadAll<Minion>(Minions).ToList();
        }

        private void Start()
        {
            _menuTheme.Play();

            if (_playerProgress.IsPlayerCreated == true)
                _continue.interactable = true;
        }

        private void OnEnable()
        {
            _newRun.onClick.AddListener(StartNewGame);
            _continue.onClick.AddListener(LoadGame);
            _garageButton.onClick.AddListener(OpenGarage);
            _settings.onClick.AddListener(OpenSettings);
            _quit.onClick.AddListener(ExitGame);
        }        

        private void OnDestroy()
        {
            _newRun.onClick.RemoveListener(StartNewGame);
            _continue.onClick.RemoveListener(LoadGame);
            _garageButton.onClick.RemoveListener(OpenGarage);
            _settings.onClick.RemoveListener(OpenSettings);
        }        

        public void ActivateFirstMinion(Button button)
        {
            _activeMinions.Add(_minions[3]);
            _minions.Remove(_minions[3]);
            button.interactable = false;
            _garage.ShowActiveMinions(_activeMinions);
        }        

        public void StartNewGame()
        {
            if (_playerProgress.IsPlayerCreated == true)
                _warningWindow.SetActive(true);
            else
                EnterTreeHouse();
        }

        private void LoadGame()
        {
            _stateMachine.Enter<LevelState, string>(_playerProgress.WorldData.Level, _playerProgress.PlayerStats.CurrentToy);
        }

        private void EnterTreeHouse()
        {
            _treeHouseWindow.SetActive(true);
            _menuWindow.SetActive(false);
            _menuTheme.Stop();
            _treeHouseTheme.Play();
        }

        private void OpenGarage()
        {
            _garageWindow.SetActive(true);
            _menuWindow.SetActive(false);
        }

        private void OpenSettings()
        {
            _settingsWindow.SetActive(true);
            _menuWindow.SetActive(false);
        }

        private void ExitGame()
            => Application.Quit();

        public void Save(PlayerProgress progress)
        {
            if (progress.IsPlayerCreated == false)
            {
                foreach (var skill in progress.PlayerSkills)
                {
                    if (skill != null)
                        skill.ResetSkill();
                }
                progress.WorldData.Stage = 1;
                Debug.Log("SaveMenu");
            }

            if (progress.WorldData.IsNewGame == true)
            {
                progress.PlayerParts.FirstLevelParts = _firstLevelParts.ToList();
                progress.PlayerParts.SecondLevelParts = _secondLevelParts.ToList();
                progress.PlayerParts.ThirdLevelParts = _thirdLevelParts.ToList();
                progress.PlayerParts.Minions = _minions.ToList();
                progress.PlayerParts.ActiveMinions = _activeMinions.ToList();
                progress.WorldData.IsNewGame = false;
            }            
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }
    }
}
