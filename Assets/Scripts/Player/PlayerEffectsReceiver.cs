
using Assets.Scripts.GameEnvironment.Battle;

namespace Assets.Scripts.Player
{
    public class PlayerEffectsReceiver : EffectsReceiver
    {

        private void Awake()
        {
            _player = GetComponent<Toy>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerMovement = GetComponent<PlayerMovement>();
            
        }

        private void Start()
        {
            _skillPanel = _player.SkillPanel;
            _skillPanel.PlayerTurnEnded += CheckEffects;
            _playerMovement.UnitMoved += ChekTrauma;
        }

        private void OnDestroy()
        {
            _skillPanel.PlayerTurnEnded -= CheckEffects;
            _playerMovement.UnitMoved -= ChekTrauma;
        }

        public void ResetEffects()
        {
            foreach (var effect in _receivedEffects)
            {
                effect.ResetEffect();
            }
        }

        protected override void ChekTrauma()
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillData != null && effect.SkillData.AttackType == AttackType.Trauma)
                {
                    _playerHealth.TakeDamage(effect.SkillData.EffectValue);
                }
            }
        }

        protected override void CheckEffects()
        {
            foreach (var effect in _receivedEffects)
            {
                if (effect.SkillData != null)
                {
                    effect.Apply(_playerHealth);
                }
            }
        }
    }
}
