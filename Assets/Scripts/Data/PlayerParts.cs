using Assets.Scripts.GameEnvironment.TreeHouse;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerParts
    {
        public List<Part> Parts;

        public PlayerParts()
        {
            Parts = new List<Part>();
        }        
    }
}
