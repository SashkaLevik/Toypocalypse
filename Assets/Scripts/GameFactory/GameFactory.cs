using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Toys;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISaveProgress> ProgressWriters { get; } = new List<ISaveProgress>();

        public List<IloadProgress> ProgressReaders { get; } = new List<IloadProgress>();

        private GameObject _toy;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateBattleHud()
        {
            GameObject battleHud = _assetProvider.Instantiate(AssetPath.BattleHud);
            return battleHud;
        }

        public Toy CreateToy(Toy prefab, GameObject at)
        {
            var toy = _assetProvider.Instantiate(prefab, at: at.transform.position);
            return toy;
        }
    }
}
