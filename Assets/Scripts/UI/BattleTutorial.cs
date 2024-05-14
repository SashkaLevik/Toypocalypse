using Assets.Scripts.Data;
using Assets.Scripts.Enemyes;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BattleTutorial : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private List<Image> _images;
        [SerializeField] private GameObject _tutorialWindow;

        private int _imageNumber;
        private PlayerProgress _playerProgress;               

        public void OpenTutorial(BaseEnemy enemy)
        {
            Invoke(nameof(StartTutorial), 0.4f);
        }

        private void StartTutorial()
        {
            if (_playerProgress.WorldData.IsFirstRun == true)
            {
                Time.timeScale = 0;
                _tutorialWindow.SetActive(true);
                StartCoroutine(WaitNextImage());
            }
        }

        private IEnumerator WaitNextImage()
        {
            while (_imageNumber != _images.Count)
            {
                ShowNext();
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return null;
            }

            _playerProgress.WorldData.IsFirstRun = false;
            Time.timeScale = 1;
            _tutorialWindow.SetActive(false);
        }

        private void ShowNext()
        {            
            _images[_imageNumber].gameObject.SetActive(true);

            if (_imageNumber > 0)
                _images[_imageNumber - 1].gameObject.SetActive(false);

            _imageNumber++;
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }

        public void Save(PlayerProgress progress)
        {
        }
    }
}
