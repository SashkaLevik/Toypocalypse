using Assets.Scripts.Data.StaticData;
using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerStats
    {
        public int Bubblegum;
        public int Plasticine;
        public int Glue;
        public int Screw;
        public float MaxHP;
        public float CurrentHP;
        public float MaxSpeed;
        public float CurrentSpeed;
        public ToyStaticData CurrentToy;

        public void ResetHP() => CurrentHP = MaxHP;

        public PlayerStats()
        {
            Bubblegum = 10;
            Plasticine = 5;
            Glue = 5;
            Screw = 0;
            MaxHP = 0;
            CurrentHP = 0;
            MaxSpeed = 0;
            CurrentSpeed = 0;
        }
    }
}
