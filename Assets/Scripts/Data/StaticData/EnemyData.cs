using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.Dice;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public List<SkillData> Skills;
        public Vector3 SpawnPosition;
        public GameObject Appear;
        public float Health;
        public float StartDefence;
    }
}
