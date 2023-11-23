using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Toys
{
    public class Toy : MonoBehaviour
    {
        [SerializeField] protected Sprite _toyImage;

        public float _health;
        public float _speed;
        public float _damage;

        public Sprite ToyImage => _toyImage;

        public void SetParameters(float health, float speed, float damage)
        {
            _health = health;
            _speed = speed;
            _damage = damage;
        }
    }
}
