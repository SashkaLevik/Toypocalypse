
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public interface IHealth
    {
        event UnityAction<float> HealthChanged;
        event UnityAction DefenceChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }
        float Defence { get; set; }

        void TakeDamage(float damage);
        void TakeDirectDamage(float damage);
        void BreakeDefence(float value);
    }
}
