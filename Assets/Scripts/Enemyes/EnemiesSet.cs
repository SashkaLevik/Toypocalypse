using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    [CreateAssetMenu(fileName = "EnemiesSet", menuName = "EnemiesSet")]
    public class EnemiesSet : ScriptableObject
    {
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private BaseEnemy[] _prefabs;
        [SerializeField] private EnemyTypeID _enemyType;

        public EnemyTypeID EnemyType => _enemyType;

        public BaseEnemy GetRandomPrefab()
        {
            int randomPrefab = Random.Range(0, _prefabs.Length);
            _enemy = _prefabs[randomPrefab];
            return _enemy;
        }
    }
}
