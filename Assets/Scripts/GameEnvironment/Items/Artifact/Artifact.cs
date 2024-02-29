using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Items.Artifact
{
    public class Artifact : MonoBehaviour
    {
        [SerializeField] private ArtifactData _data;
        [SerializeField] private Image _artifactIcon;
        [SerializeField] private Image _descriptionImage;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;

        public ArtifactData Data => _data;

        public void InitPlayer(Toy player)
        {
            _player = player;
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
        }

        public void Init(ArtifactData data)
        {
            _data = data;
            _artifactIcon.sprite = data.Icon;
        }

        public void ApplyArtifact()
        {

        }

        public void OnEnter()
        {
            _descriptionImage.gameObject.SetActive(true);
            _description.text = GetLocalizedDescription(_data);
            _name.text = GetLocalizedName(_data);
        }

        public void OnExit()
            => _descriptionImage.gameObject.SetActive(false);

        private string GetLocalizedDescription(ArtifactData data)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return data.RuDescription;
            else
                return data.EnDescription;
        }

        private string GetLocalizedName(ArtifactData data)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return data.RuName;
            else
                return data.EnName;
        }
    }
}
