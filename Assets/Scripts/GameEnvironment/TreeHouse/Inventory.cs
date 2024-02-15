using Assets.Scripts.Data.StaticData;
using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class Inventory : MonoBehaviour
    {
        private const string Parts = "Parts";

        [SerializeField] private PartDescription _description;
        [SerializeField] private TreeHouseUI _treeHouse;
        [SerializeField] private GameObject _headContainer;
        [SerializeField] private GameObject _torsoContainer;
        [SerializeField] private GameObject _armsContainer;
        [SerializeField] private GameObject _legsContainer;
        [SerializeField] private Table _table;
        [SerializeField] private Button _onTable;
        [SerializeField] private PartsMover _partsMover;

        public List<Part> _parts = new();
        public PartData _partData;
        public Part _spawnedPart;
        public Part _currentPart;
        public Part _previousPart;
        private Part _newPart;
        public GameObject HeadContainer => _headContainer;
        public GameObject TorsoContainer => _torsoContainer;
        public GameObject ArmsContainer => _armsContainer;
        public GameObject LegsContainer => _legsContainer;

        public event UnityAction<Part> PlacedOnTable;              

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
                _description.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _onTable.onClick.AddListener(RemovePart);
            _table.PartReturned += AddPart;
        }

        private void OnDisable()
        {
            _onTable.onClick.RemoveListener(RemovePart);
            _table.PartReturned -= AddPart;
        }

        public void AddPart(Part part)
        {
            _parts.Add(part);
            _spawnedPart = part;
            _spawnedPart.PartChoosed += ChoosePart;
        }

        private void RemovePart()
        {
            if (_currentPart == null)
                _treeHouse.Warning.Enable(_treeHouse.Warning.NoPartChoosed);
            else
            {
                if (_table.CanAddPart() && _table.CheckTypeMatch(_currentPart))
                {
                    _currentPart.PartChoosed -= ChoosePart;
                    _partsMover.RemovePart(_parts, _currentPart, _table.transform);
                    PlacedOnTable?.Invoke(_currentPart);
                    _description.gameObject.SetActive(false);
                    _currentPart = null;
                }
                else if(_table.CanAddPart() == false)
                    _treeHouse.Warning.Enable(_treeHouse.Warning.AllPartsChoosed);
            }                        
        }                        

        public void ChoosePart(Part part)
        {
            if (_currentPart == null)
            {
                _currentPart = part;
                _currentPart.SetEnableColor();
                _description.gameObject.SetActive(true);
                _description.SetValues(_currentPart.PartData);
            }
            else
            {
                _previousPart = _currentPart;
                _previousPart.SetDisableColor();
                _currentPart = part;
                _currentPart.SetEnableColor();
                _description.SetValues(_currentPart.PartData);
            }
        }        
    }
}
