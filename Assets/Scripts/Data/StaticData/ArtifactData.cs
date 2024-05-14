using Assets.Scripts.GameEnvironment.Items.Artifact;
using Assets.Scripts.GameEnvironment.TreeHouse;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "ArtifactData", menuName = "ArtifactData")]
    public class ArtifactData : ScriptableObject
    {
        public Artifact Prefab;
        public string RuName;
        public string EnName;
        public string RuDescription;
        public string EnDescription;
        public int Price;
        public int AppliedValue;
        public Sprite Icon;
        public ConnectingMaterial Material;
        public ArtifactType ArtifactType;
        public DiceFaceData FaceData;
    }
}
