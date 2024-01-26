using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : Health, ISaveProgress
    {
        private Toy _player;       

        private void Awake()
        {
            //_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _player = GetComponent<Toy>();
            _player.AreaChanged += OnDefenceArea;
        }

        private void Start()
        {
            //_animator = GetComponent<Animator>();
            //_maxHealth = _player.Health;
            _currentHealth = _maxHealth;
        }

        private void OnDestroy()
            => _player.AreaChanged -= OnDefenceArea;

        public void Save(PlayerProgress progress)
        {
            
        }

        public void Load(PlayerProgress progress)
        {
            _maxHealth = progress.PlayerStats.MaxHP;
        }
    }
}
