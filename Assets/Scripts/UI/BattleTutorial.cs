using Assets.Scripts.Data;
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

        private int _imageNumber;
        private PlayerProgress _playerProgress;       

        private void Start()
        {
            if (_playerProgress.WorldData.IsFirstRun == true)
            {
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
            progress.WorldData.IsFirstRun = false;
        }
    }
}
