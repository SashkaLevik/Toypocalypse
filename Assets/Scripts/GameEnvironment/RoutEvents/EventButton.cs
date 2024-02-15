using Assets.Scripts.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents
{
    public class EventButton : MonoBehaviour
    {
        private Button _button;
        private RoutEvent _routEvent;
        private RoutMap _routMap;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OpenEvent);
        }

        private void OpenEvent()
        {
            _routEvent.gameObject.SetActive(true);
            _routMap.gameObject.SetActive(false);
            _routMap.InvokEvent(_routEvent);
        }            

        public void InitEvent(RoutEvent routEvent, RoutMap routMap)
        {
            _routEvent = routEvent;
            _routMap = routMap;
        }            
    }
}
