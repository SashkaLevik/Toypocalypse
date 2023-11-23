using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Toys;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {        
        Toy Instantiate(Toy prefab, Vector3 at);
        GameObject Instantiate(string path);
    }
}
