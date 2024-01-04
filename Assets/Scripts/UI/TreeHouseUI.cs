using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using Assets.Scripts.GameEnvironment.Battle;

namespace Assets.Scripts.UI
{
    public class TreeHouseUI : MonoBehaviour
    {
        private const string Battle = "Battle";
        [SerializeField] private ToyConstructor _toyConstructor;
        [SerializeField] private Image _notChoosedWarning;
        [SerializeField] private Image _fullTableWarning;
        [SerializeField] private Image _materialWarning;
        [SerializeField] private Image _existWarning;
        [SerializeField] private Image _toyPreview;
        [SerializeField] private Image _map;
        [SerializeField] private Button _openMap;
        [SerializeField] private Transform _toyPreviewPos;

        private IGameStateMachine _stateMachine;
        public ToyStaticData _createdToyData;
        public Image NotChoosedWarning => _notChoosedWarning;
        public Image FullTableWarning => _fullTableWarning;
        public Image MaterealWarning => _materialWarning;
        public Image ExistWarning => _existWarning;

        public ToyStaticData ToyData => _createdToyData;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void Start()
        {
        }

        private void OnEnable()
        {
            _toyConstructor.ToyConstructed += OnToyConstruct;
            _openMap.onClick.AddListener(OnMapOpen);
        }

        private void OnDisable()
        {
            
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

        private void OnBattleEntered()
        {
            _stateMachine.Enter<LevelState, string>(Battle, _createdToyData);
        }

        public void EnableWarning(Image image)
        {
            image.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(image));
        }         

        public void ShowToyPreview(Toy toy)
        {
            //Instantiate(toy, _toyPreviewPos);
            _toyPreview.gameObject.SetActive(true);
            _toyPreview.sprite = toy.ToyImage;
        }

        private IEnumerator DisableWarning(Image image)
        {
            yield return new WaitForSeconds(2f);
            image.gameObject.SetActive(false);
        }                    
    }
}
