using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerParts
    {
        public List<Part> ActiveParts;
        public List<Part> FirstLevelParts;
        public List<Part> SecondLevelParts;
        public List<Part> ThirdLevelParts;
        public List<Minion> Minions;
        public List<Minion> ActiveMinions;
        public MinionData CurrentMinion;

        public PlayerParts()
        {
            ActiveParts = new List<Part>();
            FirstLevelParts = new List<Part>();
            SecondLevelParts = new List<Part>();
            ThirdLevelParts = new List<Part>();
            Minions = new List<Minion>();
            ActiveMinions = new List<Minion>();
        }        
    }
}
