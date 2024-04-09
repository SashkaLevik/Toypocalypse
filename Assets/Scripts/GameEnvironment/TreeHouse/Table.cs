using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private MaterialsPanel _materials;
        [SerializeField] private TreeHouseUI _treeHouse;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PartsMover _partsMover;
        [SerializeField] private Button _construct;
        [SerializeField] private Image _materialImage;
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private int _requiredMaterial;

        public Part _currentPart;
        public Part _previousPart;
        public ConnectingMaterial _currentMaterial;
        public ConnectingMaterial _previousMaterial;
        public List<Part> _parts = new List<Part>();
        private int _maxParts = 4;
        
        public float Health => _health;
        public float Speed => _speed;
        public List<Part> Parts => _parts;
        public int RequiredMaterial => _requiredMaterial;

        public event UnityAction<float, float, float> PartAdded;
        public event UnityAction<float, float, float> PartRemoved;
        public event UnityAction<float> MaterialAdded;
        public event UnityAction<Part> PartReturned;
        public event UnityAction ToyConstructed;

        private void Start()
        {
        }

        private void OnEnable()
        {
            _construct.onClick.AddListener(ConstructToy);
            _inventory.PlacedOnTable += AddPart;
        }

        private void OnDisable()
        {
            _construct.onClick.RemoveListener(ConstructToy);
            _inventory.PlacedOnTable -= AddPart;
        }        

        public bool CheckTypeMatch(Part part)
        {
            if (_parts.Count > 0)
            {
                foreach (var partOnTable in _parts)
                {
                    if (partOnTable.PartData.Type == part.PartData.Type)
                    {
                        _treeHouse.Warning.Enable(_treeHouse.Warning.SamePartChoosed);
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

        public void AddMaterial(ConnectingMaterial material)
        {            
            if (_currentMaterial == null)
            {
                _currentMaterial = material;
                _materialImage.gameObject.SetActive(true);
                _materialImage.sprite = _currentMaterial.Data.Icon;
                _health += _currentMaterial.Data.Health;
                _construct.gameObject.SetActive(true);
                MaterialAdded?.Invoke(_health);
            }
            else
            {
                _previousMaterial = _currentMaterial;
                _health -= _previousMaterial.Data.Health;
                _materials.ReturnMaterial(_previousMaterial);
                _currentMaterial = material;
                _materialImage.sprite = _currentMaterial.Data.Icon;
                _health += _currentMaterial.Data.Health;
                MaterialAdded?.Invoke(_health);
            }
        }
       
        public void DisableOnTutorial()
        {
            foreach (var part in _parts)
                part.DisableOnTutorial();
        }

        private void AddPart(Part part)
        {            
            _parts.Add(part);
            IncreaseValues(part.PartData.Health, part.PartData.Speed, part.PartData.MaterialAmount);
            PartAdded?.Invoke(_health, _speed, _requiredMaterial);
            part.PartChoosed += ChoosePart;
            if (_parts.Count == _maxParts) _materials.EnablePanel();
        }

        private void ChoosePart(Part part)
        {
            _currentPart = part;
            _currentPart.DoubleClicked += RemovePart;
        }

        private void RemovePart()
        {
            if (_currentMaterial != null)
            {
                _materials.ReturnMaterial(_currentMaterial);
                _materialImage.gameObject.SetActive(false);
                _health -= _currentMaterial.Data.Health;
                _materials.ResetMaterial();
                _currentMaterial = null;
            }

            DecreaseValues(_currentPart.PartData.Health, _currentPart.PartData.Speed, _currentPart.PartData.MaterialAmount);
            PartRemoved?.Invoke(_health, _speed, _requiredMaterial);
            PartReturned?.Invoke(_currentPart);
            GetSutableContainer();
            _currentPart.PartChoosed -= ChoosePart;
            _currentPart.DoubleClicked -= RemovePart;
            _parts.Remove(_currentPart);
            _currentPart = null;

            if (_parts.Count < _maxParts)
            {
                _materials.DisablePanel();
                _construct.gameObject.SetActive(false);                
            }                      
        }

        private void ConstructToy()
        {
            ToyConstructed?.Invoke();

            foreach (var part in _parts)
                part.GetComponent<Image>().gameObject.SetActive(false);
        }        

        private void IncreaseValues(float health, float speed, int material)
        {
            _health += health;
            _speed += speed;
            _requiredMaterial += material;
        }

        private void DecreaseValues(float health, float speed, int material)
        {
            _health -= health;
            _speed -= speed;
            _requiredMaterial -= material;
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
