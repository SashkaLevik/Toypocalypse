using Assets.Scripts.Data.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class PartsSpawner : MonoBehaviour
    {
        private const string Heads = "Parts/Head";
        private const string Legs = "Parts/Legs";
        private const string Torsos = "Parts/Torsos";
        private const string Arms = "Parts/Arms";

        [SerializeField] private Inventory _inventory;

        public List<Part> _heads;
        public List<Part> _torsos;
        public List<Part> _arms;
        public List<Part> _legs;
        public Part _spawnedPart;

        private void Awake()
        {
            _heads = Resources.LoadAll<Part>(Heads).ToList();
            _torsos = Resources.LoadAll<Part>(Torsos).ToList();
            _arms = Resources.LoadAll<Part>(Arms).ToList();
            _legs = Resources.LoadAll<Part>(Legs).ToList();            
        }

        private void Start()
        {
            SpawnParts();
        }

        private void SpawnParts()
        {
            GetAvalableParts(_heads, _inventory.HeadContainer.transform);
            GetAvalableParts(_torsos, _inventory.TorsoContainer.transform);
            GetAvalableParts(_arms, _inventory.ArmsContainer.transform);
            GetAvalableParts(_legs, _inventory.LegsContainer.transform);            
        }

        private void GetAvalableParts(List<Part> parts, Transform container)
        {
            foreach (var part in parts)
            {
                if (part.IsAvalable())
                {
                    _spawnedPart = Instantiate(part, container);
                    _inventory.AddPart(_spawnedPart);
                }
            }
        }
    }
}
