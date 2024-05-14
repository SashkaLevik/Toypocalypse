using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "DiceFaceData", menuName = "DiceFaceData")]
    public class DiceFaceData : ScriptableObject
    {
        public Sprite FaceImage;
        public Material Material;
        public AreaType AreaType;
        public int IncreaseValue;
        public int DecreaseValue;
    }
}
