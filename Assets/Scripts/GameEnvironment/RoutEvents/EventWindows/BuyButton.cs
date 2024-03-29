using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Artifact;
using Assets.Scripts.GameEnvironment.Items.Potions;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class BuyButton : MonoBehaviour
    {
        private PotionData _potion;
        private ArtifactData _artifact;

        public PotionData Potion => _potion;

        public ArtifactData Artifact => _artifact;

        public void GetPotion(PotionData potion)
            => _potion = potion;

        public void GetArtifact(ArtifactData artifact)
            => _artifact = artifact;
    }
}
