using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth, ISaveProgress
    {
        public float _currentHealth;
        public float _maxHealth;
        public float _defence;
        private Animator _animator;
        public Toy _toy;
        public PlayerStats _stats;
        private ISaveLoadService _saveLoadService;

        public event UnityAction HealthChanged;
        public event UnityAction Died;       

        public float CurrentHP
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                HealthChanged?.Invoke();
            }
        }

        public float MaxHP { get => _maxHealth; set => _maxHealth = value; }
        //{
        //    get => _stats.MaxHP;
        //    set => _stats.MaxHP = value;
        //}

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _toy = GetComponent<Toy>();
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _maxHealth = _toy.Health;
            _currentHealth = _maxHealth;
        }

        public void Load(PlayerProgress progress)
        {
            _stats = progress.PlayerStats;
            HealthChanged?.Invoke();
            //_currentHealth = progress.PlayerStats.CurrentHP;
        }

        public void Save(PlayerProgress progress)
        {
            progress.PlayerStats.CurrentHP = _currentHealth;
            progress.PlayerStats.MaxHP = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHP -= (damage-_defence);

            if (CurrentHP <= 0)
                Die();
        }

        private void Die()
        {
            _animator.SetTrigger("Die");
            Died?.Invoke();
        }
    }
}
