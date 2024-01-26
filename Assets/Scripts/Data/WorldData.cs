using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public string Level;
        public int Stage;
        public int LevelNumber;

        public WorldData(string level)
        {
            Level = level;
            Stage = 0;
            LevelNumber = 0;
        }
    }
}
