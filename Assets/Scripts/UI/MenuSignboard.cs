using Lean.Localization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenuSignboard : MonoBehaviour
    {
        private const string Russian = "Russian";

        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private Sprite _ruOnImage;
        [SerializeField] private Sprite _ruOffImage;
        [SerializeField] private Sprite _enOnImage;
        [SerializeField] private Sprite _enOffImage;
        [SerializeField] private Button _signboardButton; 
        [SerializeField] private AudioSource _buttonSound;

        public event UnityAction<bool> IsButtonEntered;

        private void Start()
        {
            _signboardButton.image.sprite = GetOnImage();
            if (_signboardButton.interactable == false)
                _signboardButton.image.sprite = GetOffImage();
        }        

        public void OnEnter()
        {
            if (_signboardButton.interactable == true)
            {
                _signboardButton.image.sprite = GetOffImage();
                IsButtonEntered?.Invoke(true);
            }            
        }

        public void OnExit()
        {
            if (_signboardButton.interactable == true)
            {
                _buttonSound.Play();
                _signboardButton.image.sprite = GetOnImage();
                IsButtonEntered?.Invoke(false);
            }            
        }        

        public void OnClick()
        {
            _signboardButton.image.sprite = GetOnImage();
            IsButtonEntered?.Invoke(false);
        }

        public void DetectLanguage()
        {
            if (_signboardButton.interactable == false)
                _signboardButton.image.sprite = GetOffImage();
            else
                _signboardButton.image.sprite = GetOnImage();
        }

        private Sprite GetOnImage()
        {
            if (_leanLocalization.CurrentLanguage == Russian)
                return _ruOnImage;
            else
                return _enOnImage;
        }

        private Sprite GetOffImage()
        {
            if (_leanLocalization.CurrentLanguage == Russian)
                return _ruOffImage;
            else
                return _enOffImage;
        }
    }
}
