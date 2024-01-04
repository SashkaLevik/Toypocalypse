using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected EnemyData _enemyData;

        private Toy _player;

        public Toy Player => _player;

        public EnemyData EnemyData => _enemyData;

        public event UnityAction<AreaType> AreaChanged;

        public void InitPlayer(Toy player)
            => _player = player;

        public void ChangeArea(AreaType type)
            => AreaChanged?.Invoke(type);
    }
}
