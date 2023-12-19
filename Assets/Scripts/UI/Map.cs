using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Button _home;

        public event UnityAction HomeLoaded;

        private void OnEnable()
        {
            _home.onClick.AddListener(LoadHome);
        }

        private void LoadHome()
            => HomeLoaded?.Invoke();
    }
}
