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

        public event UnityAction<ConnectingMaterial> RequiredQuantityAdded;

        private void Start()
        {
            UpdateMaterialsCount();
        }

        private void OnEnable()
        {
            foreach (var material in _materials)
            {
                material.MaterialChoosed += OnMaterialChoosed;
            }

            _table.MaterialReturned += ReturnMaterial;
        }

        private void OnDestroy()
        {
            foreach (var material in _materials)
            {
                material.MaterialChoosed -= OnMaterialChoosed;
            }

            _table.MaterialReturned -= ReturnMaterial;
        }        

        public void EnableMaterialPanel() => gameObject.SetActive(true);
        public void DisableMaterialPanel() => gameObject.SetActive(false);        

        public void ReturnMaterial(ConnectingMaterial material)
        {
            _playerMoney.AddMaterialByType(material.Data.Type, _table.RequiredMaterial);
            UpdateMaterialsCount();           
        }

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
                RequiredQuantityAdded?.Invoke(_currentMaterial);
                UpdateMaterialsCount();
            }
            else
                _treeHouse.EnableWarning(_treeHouse.MaterealWarning);          
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
