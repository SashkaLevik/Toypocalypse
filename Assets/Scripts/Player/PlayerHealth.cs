using Assets.Scripts.Data;
using Assets.Scripts.SaveLoad;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : Health, ISaveProgress
    {
        private Toy _player;

        public event UnityAction Died;

        private void Awake()
        {
            _player = GetComponent<Toy>();
            _player.AreaChanged += OnDefenceArea;
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        private void OnDestroy()
            => _player.AreaChanged -= OnDefenceArea;

        public void Heal(float amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        public void RiseDefence(float amount)
        {
            Defence += amount;
        }

        public void Save(PlayerProgress progress)
        {
            
        }

        public void Load(PlayerProgress progress)
        {
            _maxHealth = progress.PlayerStats.MaxHP;
        }

        protected override void Die()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}
