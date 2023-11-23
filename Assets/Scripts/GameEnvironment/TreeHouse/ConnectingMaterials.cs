using Assets.Scripts.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class ConnectingMaterials : MonoBehaviour
    {
        [SerializeField] private TreeHouseUI _treeHouse;
        [SerializeField] private Table _table;
        [SerializeField] private List<Material> _materials;
        [SerializeField] private TMP_Text _bubblegumCount;
        [SerializeField] private TMP_Text _plasticineCount;
        [SerializeField] private TMP_Text _glueCount;
        [SerializeField] private TMP_Text _screwCount;
        [SerializeField] private Button _bubblegum;
        [SerializeField] private Button _plasticine;
        [SerializeField] private Button _glue;
        [SerializeField] private Button _screw;

        public float _bubblegumAmount;
        public float _plasticineAmount;
        public float _glueAmount;
        public float _screwAmount;
        public Material _currentMaterial;

        public event UnityAction<Material> RequiredQuantityAdded;

        private void Start()
        {
            _plasticineCount.text = _plasticineAmount.ToString();
            _bubblegumCount.text = _bubblegumAmount.ToString();
            _glueCount.text = _glueAmount.ToString();
            _screwCount.text = _screwAmount.ToString();
        }

        private void OnEnable()
        {
            foreach (var material in _materials)
            {
                material.MaterialChoosed += OnMaterialChoosed;
            }

            _table.MaterialReturned += ReturneMaterial;
        }

        private void OnDisable()
        {
            foreach (var material in _materials)
            {
                material.MaterialChoosed -= OnMaterialChoosed;
            }

            //_table.MaterialReturned -= ReturneMaterial;
        }

        public void EnableMaterial() => gameObject.SetActive(true);
        public void DisableMaterial() => gameObject.SetActive(false);

        public void ReturneMaterial(Material material)
        {
            if (material.Data.Type == MaterialType.Bubblegum)
            {
                ReturneFromTable(_bubblegumCount, ref _bubblegumAmount);
            }                
            else if (material.Data.Type == MaterialType.Plasticine)
            {
                ReturneFromTable(_plasticineCount, ref _plasticineAmount);
            }
            else if (material.Data.Type == MaterialType.Glue)
            {
                ReturneFromTable(_glueCount, ref _glueAmount);
            }
            else if (material.Data.Type == MaterialType.Screw)
            {
                ReturneFromTable(_screwCount, ref _screwAmount);
            }
        }

        private void OnMaterialChoosed(Material material)
        {
            if (_currentMaterial == material) return;
            _currentMaterial = material;
            AddMaterial();
        }               

        private void AddMaterial()
        {
            if (_currentMaterial.Data.Type == MaterialType.Plasticine)
                AddPlasticine();
            else if (_currentMaterial.Data.Type == MaterialType.Bubblegum)
                AddBubblegum();
            else if (_currentMaterial.Data.Type == MaterialType.Glue)
                AddGlue();
            else if (_currentMaterial.Data.Type == MaterialType.Screw)
                AddScrew();
        }
        
        private void AddBubblegum()
        {
            if (CheckQuantity(_bubblegumAmount))
            {
                PutOnTable(_bubblegumCount, ref _bubblegumAmount);
            }
        }

        private void AddPlasticine()
        {
            if (CheckQuantity(_plasticineAmount))
            {
                PutOnTable(_plasticineCount, ref _plasticineAmount);
            }
        }

        private void AddGlue()
        {
            if (CheckQuantity(_glueAmount))
            {
                PutOnTable(_glueCount, ref _glueAmount);
            }
        }

        private void AddScrew()
        {
            if (CheckQuantity(_screwAmount))
            {
                PutOnTable(_screwCount, ref _screwAmount);
            }
        }

        private void ReturneFromTable(TMP_Text text, ref float amount)
        {
            amount += _table.RequiredMaterial;
            text.text = amount.ToString();
        }        

        private void PutOnTable(TMP_Text text, ref float amount)
        {
            amount -= _table.RequiredMaterial;
            text.text = amount.ToString();
            RequiredQuantityAdded?.Invoke(_currentMaterial);
        }

        private bool CheckQuantity(float amount)
        {
            if (amount >= _table.RequiredMaterial)
            {
                return true;
            }
            _treeHouse.EnableWarning(_treeHouse.MaterealWarning);
            return false;
        }
    }
}
