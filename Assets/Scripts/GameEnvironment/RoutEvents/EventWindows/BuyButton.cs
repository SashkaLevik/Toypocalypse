using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Artifact;
using Assets.Scripts.GameEnvironment.Items.Potions;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class BuyButton : MonoBehaviour
    {
        private Potion _potion;
        private Artifact _artifact;

        public Potion Potion => _potion;

        public Artifact Artifact => _artifact;

        public void GetPotion(Potion potion)
            => _potion = potion;

        public void GetArtifact(Artifact artifact)
            => _artifact = artifact;
    }
}
