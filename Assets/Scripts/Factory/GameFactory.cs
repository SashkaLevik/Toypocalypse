﻿using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISaveProgress> ProgressWriters { get; } = new List<ISaveProgress>();

        public List<ILoadProgress> ProgressReaders { get; } = new List<ILoadProgress>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateArtifactsWatcher()
        {
            GameObject artifactWatcher = _assetProvider.Instantiate(AssetPath.ArtifactWatcher);
            return artifactWatcher;
        }

        public GameObject CreteMenuHud()
        {
            GameObject menuHud = _assetProvider.Instantiate(AssetPath.MenuHud);
            RegisterProgressWatchers(menuHud);
            return menuHud;
        }

        public GameObject CreateBattleHud()
        {
            GameObject battleHud = _assetProvider.Instantiate(AssetPath.BattleHud);
            RegisterProgressWatchers(battleHud);
            return battleHud;
        }

        public GameObject CreateSkillPanel()
        {
            GameObject skillPanel = _assetProvider.Instantiate(AssetPath.SkillPanel);
            RegisterProgressWatchers(skillPanel.gameObject);
            return skillPanel;
        }

        public GameObject CreateBattleSystem()
        {
            GameObject battleSystem = _assetProvider.Instantiate(AssetPath.BattleSystem);
            RegisterProgressWatchers(battleSystem);
            return battleSystem;
        }

        public GameObject CreateToy(ToyStaticData toyData, GameObject at)
        {
            var toy = Object.Instantiate(toyData.Prefab, at.transform);
            RegisterProgressWatchers(toy.gameObject);
            return toy.gameObject;
        }        

        private void RegisterProgressWatchers(GameObject obj)
        {
            foreach (ILoadProgress progressReader in obj.GetComponentsInChildren<ILoadProgress>())
            {
                Register(progressReader);
            }
        }

        private void Register(ILoadProgress progressReader)
        {
            if (progressReader is ISaveProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }        
    }
}
