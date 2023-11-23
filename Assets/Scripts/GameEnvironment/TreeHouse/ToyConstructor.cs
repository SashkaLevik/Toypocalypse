using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Data.StaticData;
using UnityEngine.UI;
using Assets.Scripts.Toys;
using Assets.Scripts.UI;
using UnityEngine.Events;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class ToyConstructor : MonoBehaviour
    {
        [SerializeField] private TreeHouseUI _treeHouseUI;
        [SerializeField] private Table _table;       
        [SerializeField] private Toy _createdToy;
        
        public List<ToyStaticData> _toyDatas;
        private int _matchCount;
        private int _allParts = 4;

        public Toy CreatedToy => _createdToy;

        public event UnityAction<Toy> ToyConstructed;

        private void Start()
        {
        }

        private void OnEnable()
        {
            _table.ToyConstructed += IdentifyPrefab;
        }        

        private void CreateToy(Toy toy)
        {
            _treeHouseUI.ShowToyPreview(toy);
            toy.SetParameters(_table.Health, _table.Speed, _table.Damage);
        }

        private void IdentifyPrefab()
        {            
            foreach (var toyData in _toyDatas)
            {
                foreach (var partData in toyData.Parts)
                {
                    for (int i = 0; i < _table.Parts.Count; i++)
                    {
                        if (_table.Parts[i].PartData == partData) _matchCount++;
                    }
                }
                if (_matchCount == _allParts)
                {
                    _createdToy = toyData.Prefab;
                    ToyConstructed?.Invoke(_createdToy);
                }
                    
                else
                    _matchCount = 0;
            }
            CreateToy(_createdToy);
        }

        //private bool CheckMatch(List<PartData> partDatas)
        //{
        //    for (int i = 0; i < partDatas.Count; i++)
        //    {
        //        foreach (var part in _table.Parts)
        //        {
        //            if (partDatas[i] == part.PartData) _matchCount++;                    
        //        }
        //    }
        //    if (_matchCount == _allParts)
        //        return true;
        //    else
        //    {
        //        _matchCount = 0;
        //        return false;
        //    }            
        //}                       
    }
}
