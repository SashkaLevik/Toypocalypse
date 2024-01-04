using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.TreeHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerSpeed))]
    public class Toy : MonoBehaviour
    {
        [SerializeField] protected Sprite _toyImage;
        [SerializeField] private List<PartData> _parts;
        [SerializeField] private Canvas _attackPanel;
        
        public BaseEnemy _enemy;
        public SkillPanel _skillPanel;
        public float _health;
        public float _speed;

        public float Health => _health;
        public float Speed => _speed;
        public SkillPanel SkillPanel => _skillPanel;
        public Sprite ToyImage => _toyImage;
        public List<PartData> Parts => _parts;

        public BaseEnemy Enemy => _enemy;

        public event UnityAction<AreaType> AreaChanged; 

        private void Start()
        {
            _attackPanel.worldCamera = Camera.main;
        }

        public void Construct(SkillPanel skillPanel)
            => _skillPanel = skillPanel;

        public void InitEnemy(BaseEnemy enemy)
            => _enemy = enemy;

        public void SetParameters(float health, float speed)
        {
            _health = health;
            _speed = speed;
        }        

        public void ChangeArea(AreaType type)
        {
            AreaChanged?.Invoke(type);
        }        
    }
}
