using Assets.Scripts.Player;
using Assets.Scripts.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class MaterialsPanel : MonoBehaviour
    {
        [SerializeField] private TreeHouseUI _treeHouse;
        [SerializeField] private Table _table;
        [SerializeField] private PlayerMoney _playerMoney;
        [SerializeField] private List<ConnectingMaterial> _materials;
        [SerializeField] private TMP_Text _gumCount;
        [SerializeField] private TMP_Text _plasticineCount;
        [SerializeField] private TMP_Text _glueCount;
        [SerializeField] private TMP_Text _screwCount;        
        
        private int _currentMoney;
        private ConnectingMaterial _currentMaterial;

        public event UnityAction PanelEnabled;

        private void Start()
        {
            UpdateMaterialsCount();
        }

        private void OnEnable()
        {
            foreach (var material in _materials)
                material.MaterialChoosed += OnMaterialChoosed;
        }

        private void OnDestroy()
        {
            foreach (var material in _materials)
                material.MaterialChoosed -= OnMaterialChoosed;
        }        

        public void EnablePanel()
        {
            gameObject.SetActive(true);
            PanelEnabled?.Invoke();
        }

        public void DisablePanel()
            => gameObject.SetActive(false);

        public void ReturnMaterial(ConnectingMaterial material)
        {
            _playerMoney.AddMaterialByType(material.Data.Type, _table.RequiredMaterial);
            UpdateMaterialsCount();           
        }

        public void ResetMaterial()
            => _currentMaterial = null;

        private void OnMaterialChoosed(ConnectingMaterial material)
        {
            if (_currentMaterial == material) return;
            _currentMaterial = material;
            AddMaterial();
        }               

        private void AddMaterial()
        {
            _currentMoney = _playerMoney.GetValue(_currentMaterial.Data.Type);

            if (_currentMoney >= _table.RequiredMaterial)
            {
                _playerMoney.RemoveMaterialByType(_currentMaterial.Data.Type, _table.RequiredMaterial);
                _table.AddMaterial(_currentMaterial);
                UpdateMaterialsCount();
            }
            else
                _treeHouse.Warning.Enable(_treeHouse.Warning.NoMoney);          
        }                                  
        
        private void UpdateMaterialsCount()
        {
            _gumCount.text = _playerMoney.Gum.ToString();
            _plasticineCount.text = _playerMoney.Plasticine.ToString();
            _glueCount.text = _playerMoney.Glue.ToString();
            _screwCount.text = _playerMoney.Screw.ToString();
        }
    }
}
