using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField] private GameObject _attackObj;
        [SerializeField] private AudioSource _attackSound;
        [SerializeField] private AudioSource _hitSound;
        [SerializeField] private AudioSource _dieSound;

        private Animator _animator;

        private void Start()
            => _animator = GetComponent<Animator>();

        public void PlayAttack()
        {
            Invoke(nameof(PlayAttackSound), 0.2f);
            _animator.SetTrigger(Attack);
        }            

        public void PlayHit()
        {
            _animator.SetTrigger(Hit);
            _hitSound.Play();
        }            

        public void PlayDie()
        {
            _animator.SetBool(Die, true);
            _dieSound.Play();
        }            

        public void OnAttack()
            => _attackObj.SetActive(true);

        public void OffAttack()
        {
            _attackObj.SetActive(false);
        }

        private void PlayAttackSound()
            => _attackSound.Play();
    }
}
