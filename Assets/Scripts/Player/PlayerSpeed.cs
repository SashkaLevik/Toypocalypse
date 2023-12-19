using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerSpeed : MonoBehaviour
    {
        public float _currentSpeed;
        public float _maxSpeed;
        public Toy _toy;

        public event UnityAction SpeedChanged;

        public float CurrentSpeed
        {
            get => _currentSpeed;
            set
            {
                _currentSpeed = value;
                SpeedChanged?.Invoke();
            }
        }

        public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

        private void Awake()
        {
            _toy = GetComponent<Toy>();
        }

        private void Start()
        {
            _maxSpeed = _toy.Speed;
            _currentSpeed = _maxSpeed;
        }

        public void SpentAP(int amount)
        {
            CurrentSpeed -= amount;
        }

        public void RecoverAP(int amount)
        {
            CurrentSpeed += amount;
        }
    }
}
