using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class PartsSpawner : MonoBehaviour, ISaveProgress
    {
        private const string DefaultParts = "Player/DefaultParts";       

        [SerializeField] private Inventory _inventory;
        
        private Part _spawnedPart;

        public List<Part> _defaultParts;
        public List<Part> _activeParts;

        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _defaultParts = Resources.LoadAll<Part>(DefaultParts).ToList();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();            
        }

        private void Start()
        {
            SpawnParts(_defaultParts);
            SpawnParts(_activeParts);
        }                      

        private void SpawnParts(List<Part> parts)
        {
            foreach (var part in parts)
            {
                if (part.PartData.Type == PartType.Head)
                {
                    _spawnedPart = Instantiate(part, _inventory.HeadContainer.transform);
                    _inventory.AddPart(_spawnedPart);
                }
                else if (part.PartData.Type == PartType.Torso)
                {
                    _spawnedPart = Instantiate(part, _inventory.TorsoContainer.transform);
                    _inventory.AddPart(_spawnedPart);
                }
                else if (part.PartData.Type == PartType.Arms)
                {
                    _spawnedPart = Instantiate(part, _inventory.ArmsContainer.transform);
                    _inventory.AddPart(_spawnedPart);
                }
                else if (part.PartData.Type == PartType.Legs)
                {
                    _spawnedPart = Instantiate(part, _inventory.LegsContainer.transform);
                    _inventory.AddPart(_spawnedPart);
                }
            }            
        }

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            _activeParts = progress.PlayerParts.ActiveParts.ToList();
        }
    }
}
