using Assets.Scripts.Data;

namespace Assets.Scripts.Infrastructure.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
