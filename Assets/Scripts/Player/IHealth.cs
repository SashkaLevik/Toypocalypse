
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public interface IHealth
    {
        event UnityAction HealthChanged;
        event UnityAction DefenceChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }
        float Defence { get; set; }

        void TakeDamage(float damage);
    }
}
