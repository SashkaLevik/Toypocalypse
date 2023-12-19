using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.TreeHouse;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerSpeed))]
    public class Toy : MonoBehaviour
    {
        [SerializeField] protected Sprite _toyImage;
        [SerializeField] private List<PartData> _parts;
        [SerializeField] private Canvas _skillPanel;
        [SerializeField] private Canvas _attackPanel;

        public float _health;
        public float _speed;
        public float _damage;

        public float Health => _health;
        public float Speed => _speed;
        public Sprite ToyImage => _toyImage;
        public List<PartData> Parts => _parts;

        private void Start()
        {
            _skillPanel.worldCamera = Camera.main;
            _attackPanel.worldCamera = Camera.main;
        }

        public void SetParameters(float health, float speed, float damage)
        {
            _health = health;
            _speed = speed;
            _damage = damage;
        }

        private void ApplySkills()
        {

        }
    }
}
