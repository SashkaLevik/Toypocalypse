using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents
{
    public class RoutEvent : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private Button _close;

        public Sprite Icon => _icon;

        private void OnEnable()
        {
            _close.onClick.AddListener(CloseEvent);
        }

        private void CloseEvent()
        {
            gameObject.SetActive(false);
        }
    }
}
