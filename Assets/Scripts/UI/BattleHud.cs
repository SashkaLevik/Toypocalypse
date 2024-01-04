using Assets.Scripts.Player;
using Assets.Scripts.UI.Bar;
using UnityEngine;
using TMPro;
namespace Assets.Scripts.UI
{
    public class BattleHud : MonoBehaviour
    {
        //[SerializeField] protected Canvas _canvas;
        [SerializeField] protected HPBar _hpBar;
        [SerializeField] protected SpeedBar _speedBar;
        [SerializeField] protected TMP_Text _defence;

        protected IHealth _health;
        protected ISpeed _speed;        

        private void Start()
        {
            //_canvas.worldCamera = Camera.main;
            UpdateHPBar();
            UpdateSpeedBar();
        }              

        protected virtual void UpdateHPBar()
        {
            _hpBar.SetValue(_health.CurrentHP, _health.MaxHP);
        }

        protected virtual void UpdateDefence() { }

        protected virtual void UpdateSpeedBar()
            => _speedBar.SetValue(_speed.CurrentSpeed, _speed.MaxSpeed);        
    }
}
