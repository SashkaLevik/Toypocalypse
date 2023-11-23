using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public Stats HeroStats;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroStats = new Stats();
        }
    }
}
