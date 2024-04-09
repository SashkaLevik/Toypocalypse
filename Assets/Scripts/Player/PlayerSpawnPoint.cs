using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private RoutMap _routMap;
        //[SerializeField] private List<Area> _playerAreas;
        //[SerializeField] private Area _defenceArea;
        //[SerializeField] private Area _attackArea;

        private Toy _player;

       // public Area AttackArea => _attackArea;
        //public Area DefenceArea => _defenceArea;
        public RoutMap RoutMap => _routMap;
        public Toy Player => _player;
        //public List<Area> PlayerAreas => _playerAreas;

        public event UnityAction<Toy> PlayerSpawned;               

        public void SetPosition(ToyStaticData toyData)
            => transform.position = toyData.SpawnPosition;                

        public void GetPlayer(Toy player)
        {
            _player = player;
            PlayerSpawned?.Invoke(_player);
        }            
    }
}
