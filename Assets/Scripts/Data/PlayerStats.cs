using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerStats
    {
        public float Bubblegum;
        public float Plasticine;
        public float Glue;
        public float SuperGlue;
        public float CurrentHP;
        public float MaxHP;
        public void ResetHP() => CurrentHP = MaxHP;

        public PlayerStats()
        {
            Bubblegum = 5;
            Plasticine = 0;
            Glue = 0;
            SuperGlue = 0;
            CurrentHP = 0;
            MaxHP = 0;
        }
    }
}
