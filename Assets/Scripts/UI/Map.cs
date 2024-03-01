using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Map : MonoBehaviour
    {
        public const string Home = "Home";
        public const string Jungle = "Jungle";

        [SerializeField] private Button _home;
        [SerializeField] private Button _jungle;

        public event UnityAction<string> LevelLoaded;

        private void OnEnable()
        {
            _home.onClick.AddListener(LoadHome);
            //_jungle.onClick.AddListener(LoadJungle);
        }             


        private void LoadHome()
            => LevelLoaded?.Invoke(Home);        
    }
}
