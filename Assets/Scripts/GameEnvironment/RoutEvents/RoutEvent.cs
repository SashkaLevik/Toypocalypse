using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents
{
    public class RoutEvent : MonoBehaviour
    {
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected Button _close;
        [SerializeField] protected RoutMap _routMap;
        [SerializeField] protected Image _moneyWarning;
        [SerializeField] protected Image _materialWarning;
        [SerializeField] private PlayerSpawnPoint _playerSpawner;

        //protected SkillView _choosedSkill;
        protected SkillView _nextSkill;
        protected SkillPanel _skillPanel;
        protected PlayerMoney _playerMoney;
        protected PlayerHud _playerHud;
        protected Walet _walet;
        protected PlayerSpeed _playerSpeed;
        private Toy _player;

        public Sprite Icon => _icon;

        public event UnityAction EventCompleted;

        protected virtual void Start()
        {
            _player = _playerSpawner.Player;
            _skillPanel = _player.SkillPanel;
            _playerHud = _player.PlayerHud;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _playerSpeed.RecoverAP();
            _walet = _playerHud.GetComponentInChildren<Walet>();
            //_walet.EnableButtons();
            _playerMoney = _player.GetComponent<PlayerMoney>();
            //_playerMoney.MaterialChoosed += GetPayment;
            //_skillPanel.SkillChoosed += PrepareToBuff;
            //_skillPanel.ResetCooldown();
        }       

        protected virtual void PrepareToBuff(SkillView skill) { }

        protected virtual void GetPayment(ConnectingMaterial material) { }

        protected virtual void CloseEvent()
        {            
            EventCompleted?.Invoke();
        }

        protected void EnableMoneyWarning()
        {
            _moneyWarning.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(_moneyWarning));
        }

        protected void EnableMaterialWarning()
        {
            _materialWarning.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(_materialWarning));
        }

        private IEnumerator DisableWarning(Image image)
        {
            yield return new WaitForSeconds(2f);
            image.gameObject.SetActive(false);
        }
    }
}
