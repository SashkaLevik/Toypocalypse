using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class BaseEnemy : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        [SerializeField] protected EnemyData _enemyData;

        private Toy _player;
        private Animator _animator;
        private float _animationDelay = 1.2f;
        private SkillData _negativeEffect;

        public Toy Player => _player;
        public SkillData NegativeEffect => _negativeEffect;
        public EnemyData EnemyData => _enemyData;

        public event UnityAction AnimationEnded;
        public event UnityAction<AreaType> AreaChanged;
        public event UnityAction<SkillData> EffectReceived;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void InitPlayer(Toy player)
            => _player = player;

        public void ChangeArea(AreaType type)
            => AreaChanged?.Invoke(type);

        public void ApplyEffect(SkillData skillData)
        {
            _negativeEffect = skillData;
            EffectReceived?.Invoke(_negativeEffect);
        }            

        public void PlayAttack()
        {
            if (this.gameObject != null)
            {
                _animator.SetTrigger(Attack);
                StartCoroutine(EndTurn());
            }            
        }

        private IEnumerator EndTurn()
        {
            yield return new WaitForSeconds(_animationDelay);
            AnimationEnded?.Invoke();
        }            
    }
}
