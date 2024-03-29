using Assets.Scripts.Data.StaticData;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class AnimatorController : MonoBehaviour
    {
        private static readonly int ArmsAttack = Animator.StringToHash("ArmsAttack");
        private static readonly int HeadAttack = Animator.StringToHash("HeadAttack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField] private GameObject _attackObj;
        [SerializeField] private AudioSource _armsAttackSound;
        [SerializeField] private AudioSource _headAttackSound;
        [SerializeField] private AudioSource _hitSound;
        [SerializeField] private AudioSource _dieSound;

        private bool _isDead;
        private Animator _animator;

        public bool IsDead => _isDead;

        private void Start()
            => _animator = GetComponent<Animator>();
        
        public void PlayAttack(SkillData skill)
        {
            if (_isDead == true) return;

            if (skill.PartType == PartType.Arms)
            {
                _animator.SetTrigger(ArmsAttack);
                _armsAttackSound.Play();
            }
            else if (skill.PartType == PartType.Head)
            {
                _animator.SetTrigger(HeadAttack);
                _headAttackSound.Play();
            }                       
        }            

        public void PlayHit()
        {
            _animator.SetTrigger(Hit);
            _hitSound.Play();
        }            

        public void PlayDie()
        {
            _isDead = true;
            _animator.SetBool(Die, true);
            _dieSound.Play();
        }            

        public void OnAttack()
            => _attackObj.SetActive(true);

        public void OffAttack()
        {
            _attackObj.SetActive(false);
        }
    }
}
