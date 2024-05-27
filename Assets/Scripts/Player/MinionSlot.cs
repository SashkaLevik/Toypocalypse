using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.Dice;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class MinionSlot : MonoBehaviour
    {
        [SerializeField] private SkillPanel _skillPanel;
        [SerializeField] private RectTransform _slot;

        private float _appearDelay = 1.5f;
        private Toy _player;
        private PlayerHud _playerHud;
        private PlayerHealth _playerHealth;
        private BaseEnemy _enemy;
        private EnemyHealth _enemyHealth;
        private EnemyHud _enemyHud;
        private SkillData _minionSkillData;
        private AttackPanel _attackPanel;
        private Minion _minion;

        //public event UnityAction<DiceFace> AreaChanged;

        private void Start()
        {
            _skillPanel.BattleSystem.BattleEntered += Activate;
            _player = _skillPanel.Player;
            _playerHud = _skillPanel.PlayerHud;
            _playerHealth = _player.GetComponent<PlayerHealth>();
        }             

        public void InitEnemy(BaseEnemy enemy)
        {
            _enemy = enemy;
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyHud = _enemy.EnemyHud;
        }

        public void OnMinionAdded(Minion minion)
        {
            _minion = Instantiate(minion.MinionData.MinionPrefab, _slot);
            _minion.transform.SetParent(_slot);
            _minion.MinionButtonPressed += Attack;
            _minionSkillData = minion.MinionSkillData;
        }

        public void Activate(bool isInBattle)
        {
            if (_minion != null)
            {
                if (isInBattle == true)
                    _minion.Activate();
                else
                    _minion.Disactivate();
            }            
        }        

        private void Attack(Minion minion)
        {
            StartCoroutine(StartAttack());
        }

        private IEnumerator StartAttack()
        {
            _attackPanel = _skillPanel.AttackPanel;
            _minion.Appear();
            _minion.Disactivate();
            yield return new WaitForSeconds(_appearDelay);

            if (_minionSkillData.SkillType == SkillType.Attack)
                _enemyHealth.TakeDamage(_minionSkillData.Damage);
            if (_minionSkillData.SkillType == SkillType.Defence)
                _playerHealth.Defence += _minionSkillData.Defence;
            if (_minionSkillData.AttackType == AttackType.Push)
            {
                _playerHud.RollDice();
                _skillPanel.Disactivate();
                yield return new WaitWhile(() => _playerHud.Dice.IsRolling);
                _skillPanel.Activate();
            }         

            yield return new WaitForSeconds(0.2f);
        }
    }
}
