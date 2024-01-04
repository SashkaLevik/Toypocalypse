using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        //[SerializeField] private GameObject _enemySpawnPos;
        //[SerializeField] private EnemySpawner _enemySpawner;

        public Toy _player;
        public Toy Player => _player;
        //public BaseEnemy _enemy;

        //public event UnityAction<Toy> PlayerSpawned;

        private void Start()
        {
            Invoke(nameof(GetPlayer), 0.2f);
        }

        //private void OnEnable()
        //{
        //    _enemySpawner.EnemySpawned += GetEnemy;
        //}

        public void SetPosition(ToyStaticData toyData)
            => transform.position = toyData.SpawnPosition;

        //private void GetEnemy(BaseEnemy enemy)
        //{
        //    _enemy = enemy;
        //    _player.InitEnemy(_enemy);
        //}        

        private void GetPlayer()
        {
            _player = GetComponentInChildren<Toy>();
            //PlayerSpawned?.Invoke(_player);
        }            
    }
}
