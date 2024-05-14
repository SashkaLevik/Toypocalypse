using Assets.Scripts.Data;
using Assets.Scripts.SaveLoad;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerSpeed : MonoBehaviour, ISpeed, ISaveProgress
    {
        private float _currentSpeed;
        private float _maxSpeed;
        private Toy _toy;

        public event UnityAction<float> SpeedChanged;

        public float CurrentSpeed
        {
            get => _currentSpeed;
            set
            {
                _currentSpeed = value;
                SpeedChanged?.Invoke(CurrentSpeed);
            }
        }

        public float MaxSpeed
        {
            get => _maxSpeed;
            set
            {
                _maxSpeed = value;
            }
        }

        private void Awake()
        {
            _toy = GetComponent<Toy>();
        }

        private void Start()
        {
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

        public void RecoverAP(float value)
        {
            CurrentSpeed += value;
            if (CurrentSpeed > MaxSpeed) CurrentSpeed = MaxSpeed;
        }

        public void Save(PlayerProgress progress)
        {            
        }

        public void Load(PlayerProgress progress)
        {
            _maxSpeed = progress.PlayerStats.MaxSpeed;
        }
    }
}
