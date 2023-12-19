using Assets.Scripts.Data;

namespace Assets.Scripts.SaveLoad
{
    public interface ISaveProgress : ILoadProgress
    {
        public void Save(PlayerProgress progress);
    }
}
