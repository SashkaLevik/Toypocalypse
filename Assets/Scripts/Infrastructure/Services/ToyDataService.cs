using Assets.Scripts.Data.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class ToyDataService : IToyDataService
    {
        private const string ToyData = "ToyData";
        private List<ToyStaticData> _toyDatas;

        public void Load()
        {
            _toyDatas = Resources.LoadAll<ToyStaticData>(ToyData).ToList();
        }        

        public string GetToyPath(ToyStaticData toyData)
        {
            foreach (var data in _toyDatas)
            {
                if (data == toyData)
                {
                    Debug.Log(data.name);
                    return data.name;
                }
            }
            return null;
        }
    }
}
