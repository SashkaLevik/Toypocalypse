using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected EnemyData _enemyData;
        
        private float _animationDelay = 1.2f;
        private Toy _player;
        private AnimatorController _animator;
        private EnemyAI _enemyAI;
        private EnemyHealth _enemyHealth;
        private AreaType _currentAreaType;
        private AreaType _previousAreaType;
        private Area _currentArea;
        private Area _previousArea;

        public Toy Player => _player;
        public EnemyData EnemyData => _enemyData;
        public AnimatorController Animator => _animator;
        public Area Area => _currentArea;

        public event UnityAction AnimationEnded;

        private void Start()
        {
            _animator = GetComponent<AnimatorController>();
            _enemyAI = GetComponent<EnemyAI>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _currentAreaType = AreaType.Common;
        }
        
        public void InitPlayer(Toy player)
            => _player = player;

        public void ChangeArea(Area area, AreaType areaType)
        {
            _previousArea = _currentArea;
            _currentArea = area;
            _previousAreaType = _currentAreaType;
            _currentAreaType = areaType;

            if (_currentAreaType == AreaType.Attack)
            {
                _enemyAI.IncreaseDamage(area.AreaBattleValue);
            }
            else if (_currentAreaType == AreaType.Defence)
            {
                _enemyHealth.IncreaseDefence(area.AreaBattleValue);
            }
            else if (_previousAreaType == AreaType.Attack && _currentAreaType == AreaType.Common)
                _enemyAI.DecreaseDamage(_previousArea.AreaBattleValue);
            else if (_previousAreaType == AreaType.Defence && _currentAreaType == AreaType.Common)
                _enemyHealth.DecreaseDefence(_previousArea.AreaBattleValue);
        }

        public void PlayAttack()
        {
            if (this.gameObject != null)
            {
                _animator.PlayAttack();
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
