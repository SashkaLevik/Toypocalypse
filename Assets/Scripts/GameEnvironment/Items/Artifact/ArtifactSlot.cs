using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Items.Artifact
{
    public class ArtifactSlot : MonoBehaviour
    {
        private bool _isRepaired;

        public bool IsRepared => _isRepaired;

        public void RepareSlot()
            => _isRepaired = true;
    }
}
