using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Dice
{
    public class DiceFace : MonoBehaviour
    {
        [SerializeField] private AreaType _faceType;

        public AreaType FaceType => _faceType;
    }
}
