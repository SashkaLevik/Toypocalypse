using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateToy(ToyStaticData toyStaticData, GameObject at);

        GameObject CreteMenuHud();
        GameObject CreateBattleHud();
        GameObject CreateSkillPanel();
        GameObject CreateBattleSystem();
        GameObject CreateArtifactsWatcher();
        List<ISaveProgress> ProgressWriters { get; }
        List<ILoadProgress> ProgressReaders { get; }
    }
}
