using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private ConnectingMaterials _materials;
        [SerializeField] private TreeHouseUI _treeHouse;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PartsMover _partsMover;
        [SerializeField] private Button _inInventory;
        [SerializeField] private Button _construct;
        [SerializeField] private Image _materialImage;
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        //[SerializeField] private float _damage;
        [SerializeField] private float _material;

        public Part _currentPart;
        public Part _previousPart;
        public Material _currentMaterial;
        public Material _previousMaterial;
        public List<Part> _parts = new List<Part>();
        private int _maxParts = 4;


        public float Health => _health;
        public float Speed => _speed;
        //public float Damage => _damage;
        public List<Part> Parts => _parts;
        public float RequiredMaterial => _material;

        public event UnityAction<float, float, float> PartAdded;
        public event UnityAction<float, float, float> PartRemoved;
        public event UnityAction<float> MaterialAdded;
        public event UnityAction<Material> MaterialReturned;
        public event UnityAction<Part> PartReturned;
        public event UnityAction ToyConstructed;

        private void Start()
        {
        }

        private void OnEnable()
        {
            _construct.onClick.AddListener(ConstructToy);
            _inventory.PlacedOnTable += AddPart;
            _materials.RequiredQuantityAdded += OnMaterialAdded;
            _inInventory.onClick.AddListener(RemovePart);
        }

        private void OnDisable()
        {
            _construct.onClick.RemoveListener(ConstructToy);
            _inventory.PlacedOnTable -= AddPart;
            _materials.RequiredQuantityAdded -= OnMaterialAdded;
            _inInventory.onClick.RemoveListener(RemovePart);
        }        

        public bool CheckTypeMatch(Part part)
        {
            if (_parts.Count > 0)
            {
                foreach (var partOnTable in _parts)
                {
                    if (partOnTable.PartData.Type == part.PartData.Type)
                    {
                        _treeHouse.EnableWarning(_treeHouse.ExistWarning);
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CanAddPart()
        {
            if (_parts.Count < _maxParts)
                return true;
            return false;
        }

        private void OnMaterialAdded(Material material)
        {            
            if (_currentMaterial == null)
            {
                _currentMaterial = material;
                _health += _currentMaterial.Data.Health;
                _construct.gameObject.SetActive(true);
                _inInventory.gameObject.SetActive(false);
                MaterialAdded?.Invoke(_health);
            }
            else
            {
                _previousMaterial = _currentMaterial;
                _health -= _previousMaterial.Data.Health;
                MaterialReturned?.Invoke(_previousMaterial);
                _currentMaterial = material;
                _health += _currentMaterial.Data.Health;
                MaterialAdded?.Invoke(_health);
            }
        }
       
        private void AddPart(Part part)
        {            
            _parts.Add(part);
            IncreaseValues(part.PartData.Health, part.PartData.Speed, part.PartData.MaterialAmount);
            PartAdded?.Invoke(_health, _speed, _material);
            part.PartChoosed += ChoosePart;
            if (_parts.Count == _maxParts) _materials.EnableMaterial();
        }

        private void RemovePart()
        {
            if (_currentPart == null)
                _treeHouse.EnableWarning(_treeHouse.NotChoosedWarning);
            else
            {
                DecreaseValues(_currentPart.PartData.Health, _currentPart.PartData.Speed, _currentPart.PartData.MaterialAmount);

                PartRemoved?.Invoke(_health, _speed, _material);
                PartReturned?.Invoke(_currentPart);
                GetSutableContainer();
                _currentPart.PartChoosed -= ChoosePart;
                _currentPart = null;                
            }            
        }

        private void ConstructToy()
        {
            ToyConstructed?.Invoke();
        }

        private void ChoosePart(Part part)
        {
            if (_currentPart == null)
            {
                _currentPart = part;
                _currentPart.SetEnableColor();
            }
            else
            {
                _previousPart = _currentPart;
                _previousPart.SetDisableColor();
                _currentPart = part;
                _currentPart.SetEnableColor();
            }
        }

        private void IncreaseValues(float health, float speed, float material)
        {
            _health += health;
            _speed += speed;
            _material += material;
        }

        private void DecreaseValues(float health, float speed, float material)
        {
            _health -= health;
            _speed -= speed;
            _material -= material;
        }

        private void GetSutableContainer()
        {
            if (_currentPart.PartData.Type == PartType.Legs)
            {
                _partsMover.RemovePart(_parts, _currentPart, _inventory.LegsContainer.transform);
            }
            else if (_currentPart.PartData.Type == PartType.Torso)
            {
                _partsMover.RemovePart(_parts, _currentPart, _inventory.TorsoContainer.transform);
            }
            else if (_currentPart.PartData.Type == PartType.Head)
            {
                _partsMover.RemovePart(_parts, _currentPart, _inventory.HeadContainer.transform);
            }
            else if (_currentPart.PartData.Type == PartType.Arms)
            {
                _partsMover.RemovePart(_parts, _currentPart, _inventory.ArmsContainer.transform);
            }
        }        
    }
}
