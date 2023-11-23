using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public string Level;

        public WorldData(string level)
        {
            Level = level;
        }
    }
}
