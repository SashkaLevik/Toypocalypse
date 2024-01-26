using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class PartsSpawner : MonoBehaviour, ISaveProgress
    {
        private const string Heads = "Player/Parts/Head";
        private const string Legs = "Player/Parts/Legs";
        private const string Torsos = "Player/Parts/Torsos";
        private const string Arms = "Player/Parts/Arms";

        [SerializeField] private Inventory _inventory;

        private List<Part> _heads;
        private List<Part> _torsos;
        private List<Part> _arms;
        private List<Part> _legs;
        private Part _spawnedPart;

        public List<Part> _activatedParts;
        public List<Part> _disactivatedParts;

        public PlayerParts _playerParts;

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

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            //Parts = progress.PlayerParts.Parts.ToList();
        }
    }
}
