using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemySpeed : MonoBehaviour, ISpeed
    {
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _maxSpeed;

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

        private void Start()
        {
            _maxSpeed = _enemy.EnemyData.Speed;
            _currentSpeed = _maxSpeed;
        }

        public void SpentAP(float amount)
        {
            CurrentSpeed -= amount;
        }

        public void ResetAP()
        {
            CurrentSpeed = MaxSpeed;
        }
    }
}
