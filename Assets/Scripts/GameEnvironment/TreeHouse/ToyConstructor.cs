using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class ToyConstructor : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private TreeHouseUI _treeHouseUI;
        [SerializeField] private Table _table;       
        [SerializeField] private List<ToyStaticData> _toyDatas;
        
        private int _matchCount;
        private int _allParts = 4;
        private ToyStaticData _createdToyData;

        public event UnityAction<ToyStaticData> ToyConstructed;       

        private void OnEnable()
        {
            _table.ToyConstructed += IdentifyPrefab;
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
                    _createdToyData = toyData;
                    ToyConstructed?.Invoke(_createdToyData);                    
                }
                    
                else
                    _matchCount = 0;
            }
            var toy = _createdToyData.Prefab.GetComponent<Toy>();
            ShowCreatedToy(toy);
        }

        private void ShowCreatedToy(Toy toy)
        {
            _treeHouseUI.ShowToyPreview(toy);
        }

        public void Save(PlayerProgress progress)
        {
            progress.PlayerStats.MaxHP = _table.Health;
            progress.PlayerStats.MaxSpeed = _table.Speed;
            progress.PlayerStats.CurrentToy = _createdToyData;
        }

        public void Load(PlayerProgress progress)
        {
            
        }
    }
}
