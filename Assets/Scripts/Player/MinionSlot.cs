using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class MinionSlot : MonoBehaviour
    {
        [SerializeField] private SkillPanel _skillPanel;
        //[SerializeField] private Button _minionAttack;
        //[SerializeField] private Image _minionIcon;
        [SerializeField] private RectTransform _slot;

        private Toy _player;
        private PlayerHealth _playerHealth;
        private BaseEnemy _enemy;
        private EnemyHud _enemyHud;
        private EnemyMovement _enemyMovement;
        private SkillData _minionSkillData;
        private AttackPanel _attackPanel;
        private Minion _minion;

        private void Start()
        {
            _skillPanel.BattleSystem.BattleEntered += Activate;
            //_minionAttack.onClick.AddListener(Attack);
            _player = _skillPanel.Player;
            _playerHealth = _player.GetComponent<PlayerHealth>();
        }             

        public void InitEnemy(BaseEnemy enemy)
        {
            _enemy = enemy;
            _enemyMovement = _enemy.GetComponent<EnemyMovement>();
            _enemyHud = _enemy.EnemyHud;
        }

        public void OnMinionAdded(Minion minion)
        {
            //_minion = minion;
            _minion = Instantiate(minion.MinionData.MinionPrefab, _slot);
            //_minion.transform.position = _slot.transform.position;
            _minion.transform.SetParent(_slot);
            _minion.MinionButtonPressed += Attack;
            //_minionIcon.gameObject.SetActive(true);
            //_minionIcon.sprite = minion.Icon;
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
            yield return new WaitForSeconds(1.5f);

            if (_minionSkillData.SkillType == SkillType.Attack)
                _attackPanel.Damage += _minionSkillData.Damage;
            if (_minionSkillData.SkillType == SkillType.Defence)
                _playerHealth.Defence += _minionSkillData.Defence;
            if (_minionSkillData.AttackType == AttackType.Push)
                _enemyMovement.Push();
            else if (_minionSkillData.AttackType == AttackType.Pull)
                _enemyMovement.Pull();

            yield return new WaitForSeconds(0.2f);
        }
    }
}
