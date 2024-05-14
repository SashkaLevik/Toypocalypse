
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public interface ISpeed
    {
        event UnityAction<float> SpeedChanged;
        float CurrentSpeed { get; set; }
        float MaxSpeed { get; set; }
        void SpentAP(float amount);
        void ResetAP();
    }
}
