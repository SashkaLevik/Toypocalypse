using Assets.Scripts.Player;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemySpawner : MonoBehaviour
    {
        private const string HomeLevel = "Enemy/EnemiesSet/SisterRoomArea";
        private const string EnemySpawnTag = "EnemySpawnTag";

        [SerializeField] private EnemyHud _hudPrfefab;
        [SerializeField] private RoutMap _routMap;
        [SerializeField] private GameObject _playerSpawner;

        private float _endAppearTime = 1.2f;
        public Toy _player;
        private GameObject _spawnPoint;
        private List<EnemiesSet> _enemies;
        private BaseEnemy _spawnedEnemy;
        private EnemyHud _enemyHud;

        public BaseEnemy Enemy => _spawnedEnemy;

        public event UnityAction<BaseEnemy> EnemySpawned;

        private void Awake()
        {
            _enemies = Resources.LoadAll<EnemiesSet>(HomeLevel).ToList();
        }

        private void Start()
        {
            _spawnPoint = GameObject.FindWithTag(EnemySpawnTag);
        }
        
        public void SpawnEnemy(int stageNumber)
        {
            if (stageNumber == 1)
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage1, _spawnPoint));
            else if (stageNumber == 2)
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage2, _spawnPoint));
            else if (stageNumber == 3)
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage3, _spawnPoint));
            else if (stageNumber == 4)
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage4, _spawnPoint));
            else if (stageNumber == 5)
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage5, _spawnPoint));
            else if (stageNumber == 6)
                StartCoroutine(CreateEnemy(EnemyTypeID.Stage6, _spawnPoint));
        }

        private IEnumerator CreateEnemy(EnemyTypeID enemyType, GameObject at)
        {
            _player = _playerSpawner.GetComponentInChildren<Toy>();
            var randomEnemy = GetRandomEnemy<BaseEnemy>(enemyType);
            var appear = Instantiate(randomEnemy.EnemyData.Appear);
            appear.GetComponentInChildren<EnemyAppear>().AppearEnded += OnEnemySpawned;
            _spawnPoint.GetComponent<EnemySpawnPoint>().SetPosition(randomEnemy.EnemyData);
            _spawnedEnemy = Instantiate(randomEnemy, at.transform);
            _enemyHud = Instantiate(_hudPrfefab);
            _spawnedEnemy.InitHud(_enemyHud);
            _enemyHud.Init(_spawnedEnemy);
            _spawnedEnemy.InitPlayer(_player);
            yield return new WaitForSeconds(_endAppearTime);
            Destroy(appear);            
        }

        private void OnEnemySpawned()
        {
            EnemySpawned?.Invoke(_spawnedEnemy);
        }

        private T GetRandomEnemy<T>(EnemyTypeID type) where T : BaseEnemy
        {
            return (T)_enemies.Where(e => e.EnemyType == type).OrderBy(o => Random.value).First().GetRandomPrefab();
        }        
    }
}
