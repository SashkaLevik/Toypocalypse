﻿using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class MaterialLootBox : MonoBehaviour
    {
        [SerializeField] private Button _openBox;
        [SerializeField] private Image _gumIcon;
        [SerializeField] private Image _plasticineIcon;
        [SerializeField] private Image _glueIcon;
        [SerializeField] private Image _screwIcon;
        [SerializeField] private TMP_Text _gumAmount;
        [SerializeField] private TMP_Text _plasticineAmount;
        [SerializeField] private TMP_Text _glueAmount;
        [SerializeField] private TMP_Text _screwAmount;
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private BoxAnimator _boxAnimator;

        private int _gum;
        private int _plasticine;
        private int _glue;
        private int _screw;
        private PlayerMoney _playerMoney;      
       
        private void Start()
        {
            _playerMoney = _battleSystem.Player.GetComponent<PlayerMoney>();
        }

        private void OnEnable()
        {
            _openBox.onClick.AddListener(OpenBox);
            _battleSystem.StageCompleted += CloseBox;
        }

        private void OnDestroy()
        {
            _openBox.onClick.RemoveListener(OpenBox);
            _battleSystem.StageCompleted -= CloseBox;
        }

        public void CalculatePrize()
        {
            _gum = Random.Range(6, 10);
            _plasticine = Random.Range(4, 6);
            _glue = Random.Range(2, 4);
            _screw = Random.Range(0, 2);
            _gumAmount.text = _gum.ToString();
            _plasticineAmount.text = _plasticine.ToString();
            _glueAmount.text = _glue.ToString();
            _screwAmount.text = _screw.ToString();
        }

        private void CloseBox()
        {
            _gumIcon.gameObject.SetActive(false);
            _plasticineIcon.gameObject.SetActive(false);
            _glueIcon.gameObject.SetActive(false);
            _screwIcon.gameObject.SetActive(false);
            _openBox.interactable = true;
        }           

        private void OpenBox()
        {
            CalculatePrize();
            _boxAnimator.OpenBox();
            _gumIcon.gameObject.SetActive(true);
            _plasticineIcon.gameObject.SetActive(true);
            _glueIcon.gameObject.SetActive(true);
            _screwIcon.gameObject.SetActive(true);
            _playerMoney.AddMaterialByAmount(_gum, _plasticine, _glue, _screw);
            _openBox.interactable = false;
        }              
    }
}
