using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Artifact;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class ArtifactsContainer : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private List<ArtifactSlot> _artifactSlots;
        [SerializeField] private Sprite _repairedSlot;        

        private int _openSlots;
        private Artifact _artifact;
        private Transform _position;
        private Toy _player;
        private SkillPanel _skillPanel;
        private List<ArtifactData> _artifacts = new();

        public List<ArtifactData> Artifacts => _artifacts;

        public event UnityAction<ArtifactData> ArtifactAdded;

        private void Start()
        {
            _skillPanel = GetComponent<SkillPanel>();
            _player = _skillPanel.Player;
            SetSlots();
            InstantiateArtifacts();
        }

        public void AddArtifact(ArtifactData artifact)
        {
            _artifacts.Add(artifact);
            _artifact = Instantiate(artifact.Prefab, GetPosition());
            _artifact.InitPlayer(_player);
            ArtifactAdded?.Invoke(artifact);
        }

        public bool CanAddArtifact()
        {
            foreach (var slot in _artifactSlots)
            {
                if (slot.IsRepared == true && slot.GetComponentInChildren<Artifact>() == null)
                    return true;
            }
            return false;            
        }

        private void SetSlots()
        {
            for (int i = 0; i < _openSlots; i++)
            {
                _artifactSlots[i].RepareSlot();
                var slotImage = _artifactSlots[i].GetComponent<Image>();
                slotImage.sprite = _repairedSlot;
            }
        }

        private void InstantiateArtifacts()
        {
            if (_artifacts.Count > 0)
            {
                for (int i = 0; i < _artifacts.Count; i++)
                {
                    _artifact = Instantiate(_artifacts[i].Prefab, _artifactSlots[i].transform);
                    _artifact.InitPlayer(_player);
                    ArtifactAdded?.Invoke(_artifacts[i]);
                }
            }
        }     

        private Transform GetPosition()
        {
            foreach (var slot in _artifactSlots)
            {
                if (slot.IsRepared == true && slot.GetComponentInChildren<Artifact>() == null)
                {
                    _position = slot.transform;
                }
            }
            return _position;
        }

        private void RemoveArtifact(ArtifactData data)
        {
            _artifacts.Remove(data);
        }

        public void Load(PlayerProgress progress)
        {
            _artifacts = progress.PlayerStats.Artifacts.ToList();
            _openSlots = progress.PlayerStats.ArtifactSlots;
        }

        public void Save(PlayerProgress progress)
        {
            progress.PlayerStats.Artifacts = _artifacts.ToList();
        }
    }
}
