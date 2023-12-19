
using Assets.Scripts.Data.StaticData;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IToyDataService : IService
    {
        public void Load();
        public string GetToyPath(ToyStaticData data);
    }
}
