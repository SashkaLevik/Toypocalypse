using Assets.Scripts.Data;
using Assets.Scripts.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class NewGameWarning : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private MainMenu _menu;
        [SerializeField] private GameObject _warningWindow;
        [SerializeField] private Button _return;
        [SerializeField] private Button _continue;

        private PlayerProgress _playerProgress;

        private void Start()
        {
            _return.onClick.AddListener(ReturnToMenu);
            _continue.onClick.AddListener(OpenTreeHouse);
        }

        private void OpenTreeHouse()
        {
            foreach (var skill in _playerProgress.PlayerSkills)
                if (skill != null) skill.ResetSkill();

            _playerProgress.IsPlayerCreated = false;
            _menu.StartNewGame();
            _warningWindow.SetActive(false);
        }

        private void ReturnToMenu()
            => _warningWindow.SetActive(false);

        public void Save(PlayerProgress progress) { }        

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }
    }
}
