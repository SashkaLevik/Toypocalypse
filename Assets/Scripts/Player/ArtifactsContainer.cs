using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Artifact;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ArtifactsContainer : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private List<RectTransform> _artifactSlots;
        [SerializeField] private Artifact _defaultArtifact;

        private Artifact _artifact;
        private Transform _position;
        private Toy _player;
        private SkillPanel _skillPanel;
        private List<ArtifactData> _artifacts = new();

        public List<ArtifactData> Artifacts => _artifacts;

        private void Start()
        {
            _skillPanel = GetComponent<SkillPanel>();

            InstantiateArtifacts();
        }

        public void AddArtifact(Artifact artifact)
        {
            _artifacts.Add(artifact.Data);

            artifact.transform.position = GetPosition().position;
            artifact.transform.SetParent(GetPosition());
        }

        public bool CanAddArtifact()
        {
            foreach (var slot in _artifactSlots)
            {
                if (slot.GetComponentInChildren<Artifact>() == null)
                    return true;
            }
            return false;
        }

        private void InstantiateArtifacts()
        {
            if (_artifacts.Count > 0)
            {
                for (int i = 0; i < _artifacts.Count; i++)
                {
                    _defaultArtifact.Init(_artifacts[i]);
                    _artifact = Instantiate(_defaultArtifact, _artifactSlots[i]);
                    _artifact.InitPlayer(_player);
                }
            }
        }

        private void ApplyArtifact()
        {

        }

        private Transform GetPosition()
        {
            foreach (var slot in _artifactSlots)
            {
                if (slot.GetComponentInChildren<Artifact>() == null)
                {
                    _position = slot.transform;
                }
            }
            return _position;
        }

        public void Load(PlayerProgress progress)
        {
            _artifacts = progress.PlayerStats.Artifacts.ToList();
        }

        public void Save(PlayerProgress progress)
        {
            progress.PlayerStats.Artifacts = _artifacts.ToList();
        }
    }
}
