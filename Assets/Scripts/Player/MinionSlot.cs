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
        [SerializeField] private Button _minionAttack;
        [SerializeField] private Image _minionIcon;        

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
            _minionAttack.onClick.AddListener(Attack);
            _player = _skillPanel.Player;
            _playerHealth = _player.GetComponent<PlayerHealth>();
        }             

        public void InitEnemy(BaseEnemy enemy)
        {
            _enemy = enemy;
            _enemyMovement = _enemy.GetComponent<EnemyMovement>();
            _enemyHud = _enemy.GetComponentInChildren<EnemyHud>();
        }

        public void OnMinionAdded(Minion minion)
        {
            _minion = minion;
            _minionIcon.gameObject.SetActive(true);
            _minionIcon.sprite = minion.Icon;
            _minionSkillData = minion.MinionSkillData;
        }

        public void Activate(bool isInBattle)
        {
            if (isInBattle == true)
                _minionAttack.interactable = true;
            else
                _minionAttack.interactable = false;
        }        

        private void Attack()
        {
            StartCoroutine(StartAttack());
        }

        private IEnumerator StartAttack()
        {
            _attackPanel = _skillPanel.AttackPanel;
            _minion.Appear();
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
            _minionAttack.interactable = false;
        }
    }
}
