using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ArtifactsWatcher : MonoBehaviour
    {
        [SerializeField] private SkillPanel _skillPanel;
        [SerializeField] private ArtifactsContainer _artifactsContainer;
        [SerializeField] private PlayerSpawnPoint _playerSpawner;
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private MaterialLootBox _materialBox;

        public void Construct(SkillPanel skillPanel, PlayerSpawnPoint playerSpawner, BattleSystem battleSystem)
        {
            _skillPanel = skillPanel;
            _playerSpawner = playerSpawner;
            _battleSystem = battleSystem;
            _materialBox = _battleSystem.GetComponent<MaterialLootBox>();
            _artifactsContainer = _skillPanel.GetComponent<ArtifactsContainer>();
            _artifactsContainer.ArtifactAdded += ApplyArtifactEffect;
        }

        private void ApplyArtifactEffect(ArtifactData data)
        {
            if (data.ArtifactType == ArtifactType.ServoDrive)
                _skillPanel.SetMovmentAP(data.AppliedValue);
            else if (data.ArtifactType == ArtifactType.KingsShield)
            {
                foreach (var area in _playerSpawner.PlayerAreas)
                {
                    if (area.AreaType == AreaType.Defence)
                    {
                        area.ChangeIncreased(data.AppliedValue);
                    }
                }
            }
            else if (data.ArtifactType == ArtifactType.KingsSword)
            {
                foreach (var area in _playerSpawner.PlayerAreas)
                {
                    if (area.AreaType == AreaType.Attack)
                    {
                        area.ChangeIncreased(data.AppliedValue);
                    }
                }
            }
            else if (data.ArtifactType == ArtifactType.LuckeyKeychain)
            {
                _materialBox.GetLucky();
            }
        }
    }
}
