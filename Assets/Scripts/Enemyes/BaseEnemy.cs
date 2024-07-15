using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected EnemyData _enemyData;
        [SerializeField] private int _skillsAmount;

        private Toy _player;
        private AnimatorController _animator;
        private EnemyAI _enemyAI;
        private EnemyHealth _enemyHealth;
        private EnemyHud _enemyHud;

        public int SkillsAmount => _skillsAmount;
        public EnemyHud EnemyHud => _enemyHud;
        public Toy Player => _player;
        public EnemyData EnemyData => _enemyData;
        public AnimatorController Animator => _animator;

        private void Start()
        {
            _animator = GetComponent<AnimatorController>();
            _enemyAI = GetComponent<EnemyAI>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyHealth.Defence += _enemyData.StartDefence;
        }
        
        public void InitPlayer(Toy player)
            => _player = player;

        public void InitHud(EnemyHud enemyHud)
            => _enemyHud = enemyHud;                   
    }
}
