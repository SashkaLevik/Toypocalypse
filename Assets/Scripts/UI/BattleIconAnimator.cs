using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BattleIconAnimator : MonoBehaviour
    {
        private static readonly int Animate = Animator.StringToHash("Animate");

        [SerializeField] private AudioSource _animSound;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play()
        {
            _animator.SetTrigger(Animate);
            _animSound.Play();
        }
    }
}
