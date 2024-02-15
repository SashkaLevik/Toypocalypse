using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TreeHouseUI : MonoBehaviour
    {
        [SerializeField] private Warning _warning;
        [SerializeField] private ToyConstructor _toyConstructor;
        [SerializeField] private Image _toyPreview;
        [SerializeField] private Image _toyCreator;
        [SerializeField] private Image _map;
        [SerializeField] private Button _openMap;
        [SerializeField] private Button _toMenu;
        [SerializeField] private AudioSource _constructSound;
        [SerializeField] private AudioSource _treeHouseTheme;
        [SerializeField] private AudioSource _menuTheme;
        [SerializeField] private GameObject _treeHouse;
        [SerializeField] private GameObject _menu;

        private ToyStaticData _createdToyData;

        public Warning Warning => _warning;        

        public ToyStaticData ToyData => _createdToyData;                

        private void OnEnable()
        {
            _toyConstructor.ToyConstructed += OnToyConstruct;
            _openMap.onClick.AddListener(OnMapOpen);
            _toMenu.onClick.AddListener(ReturnToMenu);
        }

        private void ReturnToMenu()
        {
            _menu.SetActive(true);
            _treeHouse.SetActive(false);
            _treeHouseTheme.Stop();
            _menuTheme.Play();
        }

        private void OnDestroy()
        {
            _toyConstructor.ToyConstructed -= OnToyConstruct;
            _openMap.onClick.RemoveListener(OnMapOpen);
        }

        private IEnumerator ShowPreview()
        {
            var tempColor = _toyPreview.color;
            tempColor.a = 0f;

            while (tempColor.a < 1)
            {
                tempColor.a += 0.03f;
                _toyPreview.color = tempColor;
                yield return new WaitForSeconds(0.03f);
            }
        }

        private void OnMapOpen()
        {
            _map.gameObject.SetActive(true);
        }

        private void OnToyConstruct(ToyStaticData toyData)
        {
            _createdToyData = toyData;
            _openMap.interactable = true;
        }                              

        public void ShowToyPreview(Toy toy)
        {
            _constructSound.Play();
            _toyCreator.gameObject.SetActive(true);
            _toyPreview.gameObject.SetActive(true);
            _toyPreview.sprite = toy.ToyImage;
            StartCoroutine(ShowPreview());
        }                          
    }
}
