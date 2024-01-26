using Assets.Scripts.GameEnvironment.TreeHouse;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class PartsLootBox : MonoBehaviour
    {
        private const string Heads = "Player/Parts/Head";

        [SerializeField] private List<Part> _1LevelParts;
        [SerializeField] private List<Part> _2LevelParts;
        [SerializeField] private List<Part> _3LevelParts;

        private List<Part> _anavailableParts;

        private void Awake()
        {
            _1LevelParts = Resources.LoadAll<Part>(Heads).ToList();            
        }

        private void Start()
        {
            GetAnavailableParts();
        }

        private void ActivatePart()
        {

        }

        private void GetAnavailableParts()
        {
            foreach (var part in _1LevelParts)
            {
                if (part.IsActivated == false)
                {
                    _anavailableParts.Add(part);
                }
            }
        }
    }
}
