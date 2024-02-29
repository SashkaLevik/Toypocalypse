using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Potions;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PotionContainer : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private List<RectTransform> _potionSlots;
        [SerializeField] private Potion _defaultPotion;

        private Transform _position;
        private Toy _player;
        private SkillPanel _skillPanel;
        private Potion _potion;
        private List<PotionData> _potions = new List<PotionData>();

        private void Start()
        {
            _skillPanel = GetComponent<SkillPanel>();
            _player = _skillPanel.Player;
            InstantiatePotions();
        }

        public bool CanAddPotion()
        {
            foreach (var slot in _potionSlots)
            {
                if (slot.GetComponentInChildren<Potion>() == null)
                    return true;
            }
            return false;
        }

        public void AddPotion(Potion potion)
        {
            _potions.Add(potion.Data);

            potion.transform.position = GetPosition().position;
            potion.transform.SetParent(GetPosition());
            potion.Used += RemovePotion;
        }        

        private Transform GetPosition()
        {
            foreach (var slot in _potionSlots)
            {
                if (slot.GetComponentInChildren<Potion>() == null)
                {
                    _position = slot.transform;
                }
            }
            return _position;
        }        

        private void InstantiatePotions()
        {
            if (_potions.Count > 0)
            {
                for (int i = 0; i < _potions.Count; i++)
                {
                    _defaultPotion.Init(_potions[i]);
                    _potion = Instantiate(_defaultPotion, _potionSlots[i]);
                    _potion.InitPlayer(_player);
                    _potion.Activate();
                    _potion.Used += RemovePotion;
                }
            }                           
        }

        private void RemovePotion(PotionData data)
        {
            _potions.Remove(data);
        }

        public void Save(PlayerProgress progress)
        {
            progress.PlayerStats.Potions = _potions.ToList();
        }

        public void Load(PlayerProgress progress)
        {
            _potions = progress.PlayerStats.Potions.ToList();
        }
    }
}
