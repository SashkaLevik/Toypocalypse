using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public Vector3 SpawnPoint;
        public GameObject Appear;
        public float Health;
        public float Speed;
    }
}
