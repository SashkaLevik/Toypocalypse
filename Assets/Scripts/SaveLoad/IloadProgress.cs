using Assets.Scripts.Data;

namespace Assets.Scripts.SaveLoad
{
    public interface ILoadProgress
    {
        public void Load(PlayerProgress progress);
    }
}
