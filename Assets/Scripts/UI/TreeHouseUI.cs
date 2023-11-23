using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using Assets.Scripts.Toys;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TreeHouseUI : MonoBehaviour
    {
        private const string BattleScene = "BattleScene";

        [SerializeField] private ToyConstructor _toyConstructor;
        [SerializeField] private Image _notChoosedWarning;
        [SerializeField] private Image _fullTableWarning;
        [SerializeField] private Image _materialWarning;
        [SerializeField] private Image _existWarning;
        [SerializeField] private Image _toyPreview;
        [SerializeField] private Button _enterBattle;

        private IGameStateMachine _stateMachine;
        public Toy _battleToy;

        public Image NotChoosedWarning => _notChoosedWarning;
        public Image FullTableWarning => _fullTableWarning;
        public Image MaterealWarning => _materialWarning;
        public Image ExistWarning => _existWarning;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnEnable()
        {
            _toyConstructor.ToyConstructed += OnToyConstruct;
            _enterBattle.onClick.AddListener(OnBattleEntered);
        }

        private void OnToyConstruct(Toy toy)
        {
            _battleToy = toy;
        }

        private void OnBattleEntered()
        {
            _stateMachine.Enter<BattleState, string>(BattleScene, _battleToy);
        }

        public void EnableWarning(Image image)
        {
            image.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(image));
        }         

        public void ShowToyPreview(Toy toy)
        {
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
