using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {        
        //GameObject Instantiate(ToyStaticData toyStaticData, string path, Vector3 at);
        //GameObject InstantiateToy(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}
