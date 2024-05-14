
using Assets.Scripts.GameEnvironment.Battle;

namespace Assets.Scripts.Enemyes
{
    public class EnemyEffectsReceiver : EffectsReceiver
    {
        private void Awake()
        {
            _enemy = GetComponent<BaseEnemy>();
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemyMovement = GetComponent<EnemyMovement>();            
        }

        private void Start()
        {
            _enemyAI.EnemyTurnEnded += CheckEffects;
            _enemyMovement.UnitMoved += ChekTrauma;           
        }

        private void OnDestroy()
        {
            _enemyAI.EnemyTurnEnded -= CheckEffects;
            _enemyMovement.UnitMoved -= ChekTrauma;
        }

        protected override void ChekTrauma()
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillData != null && effect.SkillData.AttackType == AttackType.Trauma)
                {
                    _enemyHealth.TakeDamage(effect.SkillData.EffectValue);
                }
            }
        }

        protected override void CheckEffects()
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillData != null)
                {
                    effect.Apply(_enemyHealth);
                }
            }
        }
    }
}
