using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ChooseContainer : MonoBehaviour
    {
        private const string Russian = "Russian";

        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private GameObject[] _containers;
        [SerializeField] private GameObject _attachedContainer;
        [SerializeField] private Sprite _ruOnImage;
        [SerializeField] private Sprite _ruOffImage;
        [SerializeField] private Sprite _enOnImage;
        [SerializeField] private Sprite _enOffImage;
        [SerializeField] private Button _containerButton;
        [SerializeField] private ChooseContainer[] _buttons;
        [SerializeField] private AudioSource _buttonSound;

        private void Start()
        {
            _containerButton = GetComponent<Button>();            
            _containerButton.onClick.AddListener(PushButton);

            foreach (var button in _buttons)
                button._containerButton.image.sprite = button.GetOffImage();
        }

        private void OnDestroy()        
            => _containerButton.onClick.RemoveListener(PushButton);

        public void PushButton()
        {
            _buttonSound.Play();
            _attachedContainer.SetActive(true);
            _containerButton.image.sprite = GetOnImage();

            foreach (var container in _containers)
                container.SetActive(false);

            foreach (var button in _buttons)
                button._containerButton.image.sprite = button.GetOffImage();
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
