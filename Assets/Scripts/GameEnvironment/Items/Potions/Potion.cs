using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Items.Potions
{
    public class Potion : MonoBehaviour
    {
        [SerializeField] private PotionData _data;
        //[SerializeField] private Image _potionIcon;
        [SerializeField] private Image _descriptionBG;
        [SerializeField] private TMP_Text _description;

        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;
        private Button _use;

        public event UnityAction<PotionData> Used;

        private void Awake()
        {
            _use = GetComponent<Button>();
            //_potionIcon.sprite = _data.Icon;
        }

        private void OnEnable()
            => _use.onClick.AddListener(UsePotion);

        public void OnEnter()
        {
            _descriptionBG.gameObject.SetActive(true);
            _description.text = GetLocalizedDescription(_data);
        }            

        public void OnExit()
            => _descriptionBG.gameObject.SetActive(false);        

        public void InitPlayer(Toy player)
        {
            _player = player;
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
        }            

        //public void Init(PotionData potionData)
        //{
        //    _data = potionData;
        //    _potionIcon.sprite = potionData.Icon;
        //}

        public void Activate()
            => _use.interactable = true;

        private string GetLocalizedDescription(PotionData data)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return data.RuDescription;
            else
                return data.EnDescription;
        }

        private void UsePotion()
        {
            if (_data.PotionType == PotionType.Heal)
                ApplyHeal();
            else if (_data.PotionType == PotionType.Defence)
                ApplyDefence();
            else if (_data.PotionType == PotionType.AP)
                AppplyAP();
            Used?.Invoke(_data);
            Destroy(gameObject);
        }

        private void AppplyAP()
            => _playerSpeed.RecoverAP(_data.ApplyedValue);

        private void ApplyDefence()
            => _playerHealth.RiseDefence(_data.ApplyedValue);

        private void ApplyHeal()
            => _playerHealth.Heal(_data.ApplyedValue);
    }
}
