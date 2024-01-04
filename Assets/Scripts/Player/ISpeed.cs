
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public interface ISpeed
    {
        event UnityAction SpeedChanged;
        float CurrentSpeed { get; set; }
        float MaxSpeed { get; set; }
        void SpentAP(float amount);
        void RecoverAP();
    }
}
