using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class Stats
    {
        public float Bubblegum;
        public float Plasticine;
        public float Glue;
        public float SuperGlue;

        public Stats()
        {
            Bubblegum = 5;
            Plasticine = 0;
            Glue = 0;
            SuperGlue = 0;
        }
    }
}
