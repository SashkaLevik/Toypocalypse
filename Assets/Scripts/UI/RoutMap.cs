using Assets.Scripts.GameEnvironment.RoutEvents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RoutMap : MonoBehaviour
    {
        [SerializeField] private List<Button> _stageButtons;
        //[SerializeField] private List<Button> _eventButtons;
        [SerializeField] private Button _close;
        [SerializeField] private List<Button> _1StageEventButtons;
        [SerializeField] private List<Button> _2StageEventButtons;
        [SerializeField] private List<Button> _3StageEventButtons;
        [SerializeField] private List<RoutEvent> _1StageEvents;
        [SerializeField] private List<RoutEvent> _2StageEvents;
        [SerializeField] private List<RoutEvent> _3StageEvents;
        [SerializeField] private EventButtonContainers _buttonContainers;

        private int _windowIndex;
        private int _randomNumber;
        private bool _isInBattle;

        public bool IsInBattle => _isInBattle;

        public event UnityAction StageButtonPressed;
        public event UnityAction<RoutEvent> EventEntered;        

        private void Start()
        {
            CreateEvents();

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
            => _stageButtons[stage].interactable = true;       

        public void OpenNextEvents(int stage)
        {
            EndBattle();

            var container = _buttonContainers.Containers[stage - 1];

            foreach (var button in container.Buttons)
            {
                button.interactable = true;
                button.onClick.AddListener(() => DisableButtons(container.Buttons));
            }
        }

        private void EndBattle()
            => _isInBattle = false;

        private void CreateEvents()
        {
            AssigneEventWindows(_1StageEventButtons, _1StageEvents);
            AssigneEventWindows(_2StageEventButtons, _2StageEvents);
            AssigneEventWindows(_3StageEventButtons, _3StageEvents);
        }

        private void AssigneEventWindows(List<Button> buttons, List<RoutEvent> events)
        {
            foreach (var button in buttons)
            {
                _randomNumber = Random.Range(0, events.Count);
                //while (_windowIndex == _randomNumber)
                //{
                //    _randomNumber = Random.Range(0, events.Count-1);
                //}
                _windowIndex = _randomNumber;

                button.image.sprite = events[_windowIndex].Icon;
                button.onClick.AddListener(() => OpenEventWindow(_windowIndex, events));
            }
        }

        private void OpenEventWindow(int index, List<RoutEvent> routEvents)
        {
            routEvents[index].gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            EventEntered?.Invoke(routEvents[index]);
        }                        

        private void DisableButtons(List<Button> buttons)
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }        
    }
}
