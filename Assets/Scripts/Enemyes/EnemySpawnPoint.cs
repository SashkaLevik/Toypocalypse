using Assets.Scripts.Data.StaticData;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemySpawnPoint : MonoBehaviour
    {        
        public void SetPosition(EnemyData enemyData)
            => transform.position = enemyData.SpawnPosition;
    }
}
