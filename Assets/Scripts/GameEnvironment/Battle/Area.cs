﻿using Assets.Scripts.Enemyes;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private AreaType _areaType;

        public AreaType AreaType => _areaType;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Toy player))
            {
                player.ChangeArea(_areaType);
            }
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.ChangeArea(_areaType);
            }
        }        
    }
}
