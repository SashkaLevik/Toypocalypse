using Assets.Scripts.Data.StaticData;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        //[SerializeField] private GameObject _PlayerSpawner;
        public void SetPosition(EnemyData enemyData)
            => transform.position = enemyData.SpawnPosition;
    }
}
