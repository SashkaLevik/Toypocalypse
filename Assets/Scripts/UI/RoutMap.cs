using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
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
        [SerializeField] private List<Button> _eventButtons;
        [SerializeField] private GameObject _routWindow;
        [SerializeField] private Button _close;
        [SerializeField] private List<RoutEvent> _eventWindows;
        [SerializeField] private EventButtonContainers _buttonContainers;

        public int _stageNumber;
        public int _windowIndex;

        public event UnityAction<int> StageButtonPressed;

        private void Start()
        {
            CloseCompletedStages();
            //CloseCompletedEvents(_stageNumber);
        }

        private void OnEnable()
        {
            foreach (var button in _stageButtons)
                button.onClick.AddListener(() => EnterStage(button));                           
        }

        public void EnterStage(Button button)
        {
            _stageNumber++;
            StageButtonPressed?.Invoke(_stageNumber);
            button.interactable = false;
            OpenNextEvents(_stageNumber);
            _routWindow.SetActive(false);
        }

        public void AssigneEventWindow(Button button)
        {
            var vindowIndex = Random.Range(0, _eventWindows.Count);
            button.image.sprite = _eventWindows[vindowIndex].Icon;
            button.onClick.AddListener(()=>OpenEventWindow(vindowIndex));
        }

        private void OpenEventWindow(int index)
        {
            _eventWindows[index].gameObject.SetActive(true);
            _stageButtons[_stageNumber].interactable = true;
        }            

        private void OpenNextEvents(int stage)
        {
            var container = _buttonContainers.Containers[stage - 1];

            foreach (var button in container.Buttons)
            {
                button.interactable = true;
                button.onClick.AddListener(() => DisableButtons(container.Buttons));                
            }
        }

        private void CloseCompletedStages()
        {
            if (_stageNumber >= 1)
            {
                for (int i = 0; i < _stageNumber; i++)
                {
                    _stageButtons[i].interactable = false;
                }
            }

            for (int i = _stageNumber + 1; i < _stageButtons.Count; i++)
            {
                _stageButtons[i].interactable = false;
            }
        }        

        private void DisableButtons(List<Button> buttons)
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }

        //private void CloseCompletedEvents(int stage)
        //{
        //    var container = _buttonContainers.Containers;

        //    int stageNumber = stage - 1;

        //    for (int i = 0; i < _buttonContainers.Containers.Count - stageNumber; i++)
        //    {
        //        foreach (var button in container[i].Buttons)
        //        {
        //            button.interactable = false;
        //        }
        //    }

        //    for (int i = 0; i < _stageButtons.Count - stageNumber; i++)
        //    {
        //        foreach (var button in _stageButtons)
        //        {
        //            button.interactable = false;
        //        }
        //    }
        //}
    }
}
