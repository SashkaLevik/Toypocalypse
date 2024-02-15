using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class BoxAnimator : MonoBehaviour
    {
        private const string Open = "Open";
        private const string IsOpen = "IsOpen";

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void OpenBox()
        {
            _animator.SetTrigger(Open);
        }

        public void CloseBox()
        {
            _animator.SetBool(IsOpen, false);
        }
    }
}
