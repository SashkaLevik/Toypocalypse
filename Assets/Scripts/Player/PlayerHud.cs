using Assets.Scripts.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHud : BattleHud
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _APWarning;
        [SerializeField] private Image _skillPreparedWarning;
        [SerializeField] private Image _chooseSkillWarning;
        [SerializeField] private Image _changeAreaWarning;

        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;

        public Toy Player => _player;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        private void OnDestroy()
        {
            _playerHealth.DefenceChanged -= UpdateDefence;
            _playerHealth.HealthChanged -= UpdateHPBar;
            _playerSpeed.SpeedChanged -= UpdateSpeedBar;
        }        

        public void Construct(Toy player, PlayerHealth playerHealth, PlayerSpeed playerSpeed)
        {
            _player = player;
            _playerHealth = playerHealth;
            _playerSpeed = playerSpeed;
            _playerHealth.DefenceChanged += UpdateDefence;
            _playerHealth.HealthChanged += UpdateHPBar;
            _playerSpeed.SpeedChanged += UpdateSpeedBar;
        }

        public void EnableAPWarning()
        {
            _APWarning.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(_APWarning));
        }

        public void EnableSkillWarning()
        {
            _skillPreparedWarning.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(_skillPreparedWarning));
        }

        public void EnebleAreaWarning()
        {
            _changeAreaWarning.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(_changeAreaWarning));
        }

        public void EnableSkillAbsenceWarning()
        {
            _chooseSkillWarning.gameObject.SetActive(true);
            StartCoroutine(DisableWarning(_chooseSkillWarning));
        }

        protected override void UpdateDefence()
            => _defence.text = _playerHealth.Defence.ToString();

        protected override void UpdateHPBar()
            => _hpBar.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);

        protected override void UpdateSpeedBar()
            => _speedBar.SetValue(_playerSpeed.CurrentSpeed, _playerSpeed.MaxSpeed);        

        private IEnumerator DisableWarning(Image image)
        {
            yield return new WaitForSeconds(2f);
            image.gameObject.SetActive(false);
        }
    }
}
