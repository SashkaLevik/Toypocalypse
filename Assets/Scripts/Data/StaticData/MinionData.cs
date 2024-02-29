using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "MinionData", menuName = "MinionData")]
    public class MinionData : ScriptableObject
    {
        public Minion MinionPrefab;
    }
}
