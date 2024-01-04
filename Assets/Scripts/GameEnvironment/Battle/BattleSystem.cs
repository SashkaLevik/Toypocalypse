using Assets.Scripts.Enemyes;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;        

        private Toy _player;
        private BaseEnemy _enemy;
        private EnemyAI _enemyAI;
        private PlayerSpeed _playerSpeed;
        private EnemySpeed _enemySpeed;
        public SkillPanel _skillPanel;
        public AttackPanel _attackPanel;
        public BattleState _state;

        private void Start()
        {
            StartCoroutine(SetupBattle());
        }

        private void OnEnable()
        {
            _enemySpawner.EnemySpawned += GetEnemy;
        }

        private IEnumerator SetupBattle()
        {
            yield return new WaitForSeconds(0.4f);
            GetPlayerStats();
            PlayerTurn();            
        }

        private void PlayerTurn()
            => _skillPanel.SkillPlayed += EnemyTurn;

        private void EnemyTurn()
        {
            _enemyAI.ChooseAction();
            Debug.Log("EnemyTurn");
        }

        private void GetPlayerStats()
        {
            _player = _playerSpawner.Player;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _skillPanel = _player.SkillPanel;
            _attackPanel = _player.GetComponentInChildren<AttackPanel>();
        }

        private void GetEnemy(BaseEnemy enemy)
        {
            _enemy = enemy;
            _player.InitEnemy(_enemy);
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemySpeed = _enemy.GetComponent<EnemySpeed>();
        }

    }
}
