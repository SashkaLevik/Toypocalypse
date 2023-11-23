using Assets.Scripts.Data;

namespace Assets.Scripts.SaveLoad
{
    public interface ISaveProgress : IloadProgress
    {
        public void Save(PlayerProgress progress);
    }
}
