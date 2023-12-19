using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemySpawner : MonoBehaviour
    {
        private const string Sister = "EnemiesSet/SisterRoomArea";

        [SerializeField] private RoutMap _routMap;
        [SerializeField] private Transform _spawnPoint;

        private int _stageNumber;
        private List<EnemiesSet> _enemies;
        private BaseEnemy _spawnedEnemy;
        private float _endAppearTime = 1.2f;

        private void Awake()
        {
            _enemies = Resources.LoadAll<EnemiesSet>(Sister).ToList();
        }

        private void Start()
        {
        }

        private void OnEnable()
        {
            _routMap.StageButtonPressed += SpawnEnemy;
        }                 

        public void SpawnEnemy(int stageNumber)
        {
            _stageNumber = stageNumber;
            if (_stageNumber == 1)
            {
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage1));                
            }
            else if (_stageNumber == 2)
            {
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage2));
            }
            else if (_stageNumber == 3)
            {
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage3));
            }
        }

        private IEnumerator CreateEnemy(EnemyTypeID enemyType)
        {
            var randomEnemy = GetRandomEnemy<BaseEnemy>(enemyType);
            var appear = Instantiate(randomEnemy.EnemyData.Appear);
            yield return new WaitForSeconds(_endAppearTime);
            Destroy(appear);
            _spawnedEnemy = Instantiate(randomEnemy, randomEnemy.EnemyData.SpawnPoint, Quaternion.identity);
        }

        private T GetRandomEnemy<T>(EnemyTypeID type) where T : BaseEnemy
        {
            return (T)_enemies.Where(e => e.EnemyType == type).OrderBy(o => Random.value).First().GetRandomPrefab();
        }        
    }
}
