using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents
{
    public class RoutEvent : MonoBehaviour
    {
        [SerializeField] protected Warning _warning;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected Button _close;
        [SerializeField] protected RoutMap _routMap;        
        [SerializeField] protected int _plasticinePaymentAmount;
        [SerializeField] protected int _gluePaymentAmount;
        [SerializeField] protected int _screwPaymentAmount;
        [SerializeField] private PlayerSpawnPoint _playerSpawner;

        //protected SkillView _choosedSkill;
        protected Toy _player;
        protected SkillView _nextSkill;
        protected SkillPanel _skillPanel;
        protected PlayerMoney _playerMoney;
        protected PlayerHud _playerHud;
        protected Walet _walet;
        protected PlayerSpeed _playerSpeed;
        protected PlayerHealth _playerHealth;

        public Sprite Icon => _icon;

        public event UnityAction EventCompleted;

        protected virtual void Start()
        {
            _player = _playerSpawner.Player;
            _skillPanel = _player.SkillPanel;
            _playerHud = _player.PlayerHud;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerSpeed.ResetAP();
            _walet = _skillPanel.GetComponent<Walet>();
            _playerMoney = _player.GetComponent<PlayerMoney>();
            _skillPanel.ResetCooldown();
            _walet.EnableButtons();
        }

        protected int GetPaymentValue(ConnectingMaterial material)
        {
            if (material.Data.Type == MaterialType.Plasticine) return _plasticinePaymentAmount;
            else if (material.Data.Type == MaterialType.Glue) return _gluePaymentAmount;
            else if (material.Data.Type == MaterialType.Screw) return _screwPaymentAmount;
            return 0;
        }

        protected virtual void PrepareToBuff(SkillView skill) { }

        protected virtual void GetPayment(ConnectingMaterial material) { }

        protected virtual void CloseEvent()
        {            
            EventCompleted?.Invoke();
            _walet.DisableButtons();
        }        
    }
}
