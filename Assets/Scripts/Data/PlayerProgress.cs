using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerStats PlayerStats;
        public PlayerParts PlayerParts;
        public List<SkillData> PlayerSkills;
        public bool IsPlayerCreated;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerStats = new PlayerStats();
            PlayerParts = new PlayerParts();
            PlayerSkills = new List<SkillData>();
            IsPlayerCreated = false;
        }
    }
}
