﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        //public GameObject SpawnPoint;
        public List<SkillData> Skills;
        public Vector3 SpawnPosition;
        public GameObject Appear;
        public float Health;
        public float Speed;
    }
}
