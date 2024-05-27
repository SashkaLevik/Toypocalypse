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

        //public void ChangeArea(Area area, AreaType areaType)
        //{
        //    _previousArea = _currentArea;
        //    _currentArea = area;
        //    _previousAreaType = _currentAreaType;
        //    _currentAreaType = areaType;

        //    if (_currentAreaType == AreaType.Attack)
        //    {
        //        _enemyAI.IncreaseDamage(area.IncreasedValue);
        //    }
        //    else if (_currentAreaType == AreaType.Defence)
        //    {
        //        _enemyHealth.IncreaseDefence(area.IncreasedValue);
        //    }
        //    else if (_previousAreaType == AreaType.Attack && _currentAreaType == AreaType.Common)
        //        _enemyAI.DecreaseDamage(_previousArea.IncreasedValue);
        //    else if (_previousAreaType == AreaType.Defence && _currentAreaType == AreaType.Common)
        //        _enemyHealth.DecreaseDefence(_previousArea.IncreasedValue);
        //}        

        //private IEnumerator EndTurn()
        //{
        //    yield return new WaitForSeconds(_animationDelay);
        //    AnimationEnded?.Invoke();
        //}            
    }
}
