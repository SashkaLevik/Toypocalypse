using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemyes
{
    public class EnemyAppear : MonoBehaviour
    {
        public event UnityAction AppearEnded;

        public void EndAppear()
            => AppearEnded?.Invoke();
    }
}
