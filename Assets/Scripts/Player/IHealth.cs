
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public interface IHealth
    {
        event UnityAction HealthChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }
        void TakeDamage(int damage);
    }
}
