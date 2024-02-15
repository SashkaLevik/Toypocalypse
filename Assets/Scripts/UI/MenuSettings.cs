using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenuSettings : MonoBehaviour
    {
        [SerializeField] private Button _return;
        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _settingsWindow;
        [SerializeField] private List<MenuSignboard> _menuButtons;               

        private void OnEnable()
        {
            _return.onClick.AddListener(ReturnToMenu);
        }

        private void OnDestroy()
        {
            _return.onClick.RemoveListener(ReturnToMenu);
        }

        private void ReturnToMenu()
        {
            foreach (var button in _menuButtons)
                button.DetectLanguage();

            _menuWindow.SetActive(true);
            _settingsWindow.SetActive(false);            
        }       
    }
}
