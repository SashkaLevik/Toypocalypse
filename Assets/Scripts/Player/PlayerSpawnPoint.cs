using Assets.Scripts.Data.StaticData;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private RoutMap _routMap;

        private Toy _player;

        public RoutMap RoutMap => _routMap;
        public Toy Player => _player;

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
