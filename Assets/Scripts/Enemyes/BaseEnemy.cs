using Assets.Scripts.Data.StaticData;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected EnemyData _enemyData;

        public EnemyData EnemyData => _enemyData;
    }
}
