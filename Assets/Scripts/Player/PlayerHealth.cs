using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : Health//MonoBehaviour, IHealth, ISaveProgress
    {
        private Toy _player;

        //public float _currentHealth;
        //public float _maxHealth;
        //public float _defence;
        //public float _constantDefence = 1;
        //public float _defendingDamage;
        //private bool _isDefending;
        //private Animator _animator;
        //public Toy _player;
        //public PlayerStats _stats;
        //private ISaveLoadService _saveLoadService;

        //public event UnityAction HealthChanged;
        //public event UnityAction DefenceChanged;
        //public event UnityAction Died;

        //public float Defence
        //{
        //    get => _defence;
        //    set
        //    {
        //        _defence = value;
        //        DefenceChanged?.Invoke();
        //    }
        //}

        //public float CurrentHP
        //{
        //    get => _currentHealth;
        //    set
        //    {
        //        _currentHealth = value;
        //        HealthChanged?.Invoke();
        //    }
        //}

        //public float MaxHP { get => _maxHealth; set => _maxHealth = value; }
        ////{
        ////    get => _stats.MaxHP;
        ////    set => _stats.MaxHP = value;
        ////}

        private void Awake()
        {
            //_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _player = GetComponent<Toy>();
            _player.AreaChanged += OnDefenceArea;
        }        

        private void Start()
        {
            //_animator = GetComponent<Animator>();
            _maxHealth = _player.Health;
            _currentHealth = _maxHealth;
        }

        private void OnDestroy()
            => _player.AreaChanged -= OnDefenceArea;

        //public void TakeDamage(float damage)
        //{
        //    if (_isDefending)
        //    {
        //        _defendingDamage = damage - _defence;

        //        if (_defendingDamage < 0) _defendingDamage = 0;

        //        Defence -= damage;
        //        if (Defence < 1) Defence = 1;

        //        CurrentHP -= _defendingDamage;
        //    }
        //    else
        //    {
        //        _defendingDamage = damage - _defence;

        //        if (_defendingDamage < 0) _defendingDamage = 0;

        //        Defence -= damage;
        //        if (Defence < 0) Defence = 0;

        //        CurrentHP -= _defendingDamage;
        //    }
                       

        //    //if (CurrentHP <= 0)
        //    //    Die();
        //}

        //public void IncreaseDefence(float defence)
        //    => Defence += defence;

        //public void DecreaseDefence(float defence)
        //{
        //    if (Defence > 0)
        //    {
        //        Defence -= defence;
        //    }
        //}            

        //public void Load(PlayerProgress progress)
        //{
        //    _stats = progress.PlayerStats;
        //    HealthChanged?.Invoke();
        //    //_currentHealth = progress.PlayerStats.CurrentHP;
        //}

        //public void Save(PlayerProgress progress)
        //{
        //    progress.PlayerStats.CurrentHP = _currentHealth;
        //    progress.PlayerStats.MaxHP = _maxHealth;
        //}

        //private void OnDefenceArea(AreaType areaType)
        //{
        //    if (areaType == AreaType.Defence)
        //    {
        //        IncreaseDefence(_constantDefence);
        //        _isDefending = true;
        //    }
        //    else
        //    {
        //        DecreaseDefence(_constantDefence);
        //        _isDefending = false;
        //    }
        //}

        //private void Die()
        //{
        //    _animator.SetTrigger("Die");
        //    Died?.Invoke();
        //}
    }
}
