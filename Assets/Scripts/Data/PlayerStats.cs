using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Potions;
using System;
using System.Collections.Generic;

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
        public List<PotionData> Potions;
        public List<ArtifactData> Artifacts;

        public void ResetHP() => CurrentHP = MaxHP;

        public PlayerStats()
        {
            Bubblegum = 20;
            Plasticine = 0;
            Glue = 5;
            Screw = 0;
            MaxHP = 0;
            CurrentHP = 0;
            MaxSpeed = 0;
            CurrentSpeed = 0;
            Potions = new List<PotionData>();
            Artifacts = new List<ArtifactData>();
        }
    }
}
