using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Toys;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameFactory
{
    public interface IGameFactory : IService
    {
        Toy CreateToy(Toy prefab, GameObject at);
        GameObject CreateBattleHud();

        List<ISaveProgress> ProgressWriters { get; }
        List<IloadProgress> ProgressReaders { get; }
    }
}
