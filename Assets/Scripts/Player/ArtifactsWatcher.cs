using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.GameEnvironment.Dice;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ArtifactsWatcher : MonoBehaviour
    {
        [SerializeField] private SkillPanel _skillPanel;
        [SerializeField] private ArtifactsContainer _artifactsContainer;
        [SerializeField] private PlayerHud _playerHud;
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private MaterialLootBox _materialBox;
        [SerializeField] private AreaDice _playerDice;
        [SerializeField] private DiceLayout _diceLayout;

        public void Construct(SkillPanel skillPanel, PlayerHud playerHud, BattleSystem battleSystem)
        {
            _skillPanel = skillPanel;
            _playerHud = playerHud;
            _battleSystem = battleSystem;
            _diceLayout = _skillPanel.GetComponent<DiceLayout>();
            _materialBox = _battleSystem.GetComponent<MaterialLootBox>();
            _artifactsContainer = _skillPanel.GetComponent<ArtifactsContainer>();
            _playerHud.DiceActivated += InitDice;
            _artifactsContainer.ArtifactAdded += ApplyArtifactEffect;
        }

        private void InitDice(AreaDice dice)
        {
            _playerDice = dice;
        }

        private void ApplyArtifactEffect(ArtifactData data)
        {
            if (data.ArtifactType == ArtifactType.ServoDrive)
            {
                foreach (var skill in _skillPanel.PlayerSkills)
                {
                    if (skill.SkillData.PartType == PartType.Legs)
                    {
                        skill.DecreaseAP(data.AppliedValue);
                    }
                }
            }
            else if (data.ArtifactType == ArtifactType.KingsShield)
            {
                _playerDice.AddFace(data.FaceData);
                _diceLayout.UpdateFaces(_playerDice);
            }
            else if (data.ArtifactType == ArtifactType.KingsSword)
            {
                
            }
            else if (data.ArtifactType == ArtifactType.LuckeyKeychain)
            {
                _materialBox.GetLucky();
            }
        }
    }
}
