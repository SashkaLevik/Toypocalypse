using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Toys;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "ToyData", menuName = "ToyData")]
    public class ToyStaticData : ScriptableObject
    {
        public Toy Prefab;
        public List<PartData> Parts;
    }
}
