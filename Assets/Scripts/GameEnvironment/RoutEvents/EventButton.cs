using Assets.Scripts.UI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents
{
    public class EventButton : MonoBehaviour
    {
        [SerializeField] private RoutMap _routMap;
        [SerializeField] private Button _button;

        public event UnityAction EventButtonPressed; 

        private void Start()
        {
            _routMap.AssigneEventWindow(_button);
            EventButtonPressed?.Invoke();            
        }        
    }
}
