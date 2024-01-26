using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameEnvironment.TreeHouse;
using System;

namespace Assets.Scripts.Player
{
    public class Walet : MonoBehaviour
    {
        [SerializeField] private List<ConnectingMaterial> _materials;
        [SerializeField] private TMP_Text _plasticineAmount;
        [SerializeField] private TMP_Text _glueAmount;
        [SerializeField] private TMP_Text _screwAmount;
        [SerializeField] private Button _plasticine;
        [SerializeField] private Button _glue;
        [SerializeField] private Button _screw;

        private int _currentMoney;
        private Toy _player;
        private PlayerMoney _playerMoney;
        private ConnectingMaterial _currentMaterial;

        private void Start()
        {
            _player = GetComponentInParent<PlayerHud>().Player;
            _playerMoney = _player.GetComponent<PlayerMoney>();
            _playerMoney.MoneyChanged += UpdateMaterials;
            UpdateMaterials();

            foreach (var material in _materials)
                material.MaterialChoosed += _playerMoney.OnMaterialChoosed;
        }                

        public void EnableButtons()
        {
            foreach (var material in _materials)
            {
                material.MaterialButton.interactable = true;
            }
        }

        public void DisableButtons()
        {
            foreach (var material in _materials)
            {
                material.MaterialButton.interactable = false;
            }
        }

        private void UpdateMaterials()
        {
            _plasticineAmount.text = _playerMoney.Plasticine.ToString();
            _glueAmount.text = _playerMoney.Glue.ToString();
            _screwAmount.text = _playerMoney.Screw.ToString();
        }
    }
}
