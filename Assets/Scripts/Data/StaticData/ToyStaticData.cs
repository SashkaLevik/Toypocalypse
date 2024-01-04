using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "ToyData", menuName = "ToyData")]
    public class ToyStaticData : ScriptableObject
    {
        //public GameObject SpawnPoint;
        public Vector3 SpawnPosition;
        public Sprite toyImage;
        public Toy Prefab;
        public List<PartData> Parts;
    }
}
