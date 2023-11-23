using Assets.Scripts.GameEnvironment.TreeHouse;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "Prefabs", menuName = "PartsSet")]
    public class PrefabsSet : ScriptableObject
    {
        [SerializeField] private Part _partPrefab;
        [SerializeField] private List<Part> _prefabs;
        [SerializeField] private PartType _partType;

        //public int PrefabsAmount => _prefabs.Count;

        public List<Part> Prefabs => _prefabs;

        public PartType PartType => _partType;

        public List<Part> GetPartPrefabs()
        {            
            return _prefabs;
        }
    }
}
