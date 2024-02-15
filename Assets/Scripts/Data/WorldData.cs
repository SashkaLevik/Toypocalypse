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
        public bool IsNewGame;
        public bool IsFirstRun;

        public WorldData(string level)
        {
            Level = level;
            Stage = 1;
            LevelNumber = 1;
            IsNewGame = true;
            IsFirstRun = true;
        }
    }
}
