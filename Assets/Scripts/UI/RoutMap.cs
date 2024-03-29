using Assets.Scripts.GameEnvironment.RoutEvents;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Player;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RoutMap : MonoBehaviour
    {
        private const string MenuScene = "Menu";

        [SerializeField] private PlayerSpawnPoint _playerSpawn;
        [SerializeField] private List<Button> _stageButtons;
        [SerializeField] private List<Button> _1StageEventButtons;
        [SerializeField] private List<Button> _2StageEventButtons;
        [SerializeField] private List<Button> _3StageEventButtons;
        [SerializeField] private List<Button> _4StageEventButtons;
        [SerializeField] private List<Button> _5StageEventButtons;
        [SerializeField] private List<RoutEvent> _1StageEvents;
        [SerializeField] private List<RoutEvent> _2StageEvents;
        [SerializeField] private List<RoutEvent> _3StageEvents;
        [SerializeField] private List<RoutEvent> _4StageEvents;
        [SerializeField] private List<RoutEvent> _5StageEvents;
        [SerializeField] private EventButtonContainers _buttonContainers;
        [SerializeField] private TMP_Text _partsCount;

        private bool _isInBattle;
        private Toy _player;
        private List<RoutEvent> _1StageShuffled = new();
        private List<RoutEvent> _2StageShuffled = new();
        private List<RoutEvent> _3StageShuffled = new();
        private List<RoutEvent> _4StageShuffled = new();
        private List<RoutEvent> _5StageShuffled = new();
        private IGameStateMachine _stateMachine;

        public bool IsInBattle => _isInBattle;

        public event UnityAction StageButtonPressed;
        public event UnityAction<RoutEvent> EventEntered;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void Start()
        {
            Shuffle();
            CreateEvents();
            _playerSpawn.PlayerSpawned += CheckAvalaibleParts;
            
            foreach (var button in _stageButtons)
                button.onClick.AddListener(() => EnterStage(button));
        }        

        private void OnDestroy()
        {
            foreach (var button in _stageButtons)
                button.onClick.RemoveListener(() => EnterStage(button));
        }                           

        public void EnterStage(Button button)
        {
            _isInBattle = true;
            StageButtonPressed?.Invoke();
            button.interactable = false;
            this.gameObject.SetActive(false);
        }

        public void OpenStage(int stage)
            => _stageButtons[stage - 1].interactable = true;

        public void InvokEvent(RoutEvent routEvent)
            => EventEntered?.Invoke(routEvent);

        public void OpenNextEvents(int stage)
        {
            EndBattle();

            if (stage > _buttonContainers.Containers.Count) return;

            var container = _buttonContainers.Containers[stage - 1];


            foreach (var button in container.Buttons)
            {
                button.interactable = true;
                button.onClick.AddListener(()=>Disable(button));
                button.onClick.AddListener(() => DisableButtons(container.Buttons));
            }
        }

        private void GetParts()
            => _partsCount.text = _player.InactiveParts.Count.ToString();

        private void CheckAvalaibleParts(Toy player)
        {
            _player = player;
            Invoke(nameof(GetParts), 0.5f);
        }        

        private void EndBattle()
            => _isInBattle = false;

        private void CreateEvents()
        {
            AssigneEventWindows(_1StageEventButtons, _1StageShuffled);
            AssigneEventWindows(_2StageEventButtons, _2StageShuffled);
            AssigneEventWindows(_3StageEventButtons, _3StageShuffled);
            AssigneEventWindows(_4StageEventButtons, _4StageShuffled);
            AssigneEventWindows(_5StageEventButtons, _5StageShuffled);
        }

        private void Shuffle()
        {
            _1StageShuffled = _1StageEvents.OrderBy(x => Random.value).ToList();
            _2StageShuffled = _2StageEvents.OrderBy(x => Random.value).ToList();
            _3StageShuffled = _3StageEvents.OrderBy(x => Random.value).ToList();
            _4StageShuffled = _4StageEvents.OrderBy(x => Random.value).ToList();
            _5StageShuffled = _5StageEvents.OrderBy(x => Random.value).ToList();
        }

        private void AssigneEventWindows(List<Button> buttons, List<RoutEvent> events)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].image.sprite = events[i].Icon;
                buttons[i].GetComponent<EventButton>().InitEvent(events[i], this);
            }           
        }                                             

        private void DisableButtons(List<Button> buttons)
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }

        private void Disable(Button button)
            => button.interactable = false;
    }
}
