using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerMoney : MonoBehaviour, ISaveProgress
    {
        private int _gum;
        private int _plasticine;
        private int _glue;
        private int _screw;
        private int _currentMaterialAmount;

        private ISaveLoadService _saveLoadService;
        private ConnectingMaterial _currentMaterial;

        public int Gum => _gum;
        public int Plasticine => _plasticine;
        public int Glue => _glue;
        public int Screw => _screw;
        public int CurrentMaterialAmount => _currentMaterialAmount;
        public ConnectingMaterial CurrentMaterial => _currentMaterial;

        public event UnityAction MoneyChanged;
        public event UnityAction<ConnectingMaterial> MaterialChoosed;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void OnMaterialChoosed(ConnectingMaterial material)
        {
            if (_currentMaterial == material) return;
            _currentMaterial = material;
            _currentMaterialAmount = GetValue(_currentMaterial.Data.Type);
            MaterialChoosed?.Invoke(_currentMaterial);
        }

        public void Load(PlayerProgress progress)
        {
            _gum = progress.PlayerStats.Bubblegum;
            _plasticine = progress.PlayerStats.Plasticine;
            _glue = progress.PlayerStats.Glue;
            _screw = progress.PlayerStats.Screw;
        }

        public void Save(PlayerProgress progress)
        {
            progress.PlayerStats.Bubblegum = _gum;
            progress.PlayerStats.Plasticine = _plasticine;
            progress.PlayerStats.Glue = _glue;
            progress.PlayerStats.Screw = _screw;
        }

        public void AddMaterialByAmount(int gum, int plasticine, int glue, int screw)
        {
            _gum += gum;
            _plasticine += plasticine;
            _glue += glue;
            _screw += screw;
            MoneyChanged?.Invoke();
            _saveLoadService.SaveProgress();
        }

        public void AddMaterialByType(MaterialType type, int amount)
        {
            if (type == MaterialType.Bubblegum) _gum += amount;
            else if (type == MaterialType.Plasticine) _plasticine += amount;
            else if (type == MaterialType.Glue) _glue += amount;
            else if (type == MaterialType.Screw) _screw += amount;
            MoneyChanged?.Invoke();
        }

        public void RemoveMaterialByType(MaterialType type, int amount)
        {
            if (type == MaterialType.Bubblegum) _gum -= amount;
            else if (type == MaterialType.Plasticine) _plasticine -= amount;
            else if (type == MaterialType.Glue) _glue -= amount;
            else if (type == MaterialType.Screw) _screw -= amount;
            MoneyChanged?.Invoke();
        }

        public int GetValue(MaterialType type)
        {
            if (type == MaterialType.Bubblegum) return _gum;
            else if (type == MaterialType.Plasticine) return _plasticine;
            else if (type == MaterialType.Glue) return _glue;
            else if (type == MaterialType.Screw) return _screw;
            return 0;
        }

        public void SaveMoney()
            => _saveLoadService.SaveProgress();
    }
}
