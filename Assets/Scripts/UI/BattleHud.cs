using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.GameEnvironment.Dice;
using Assets.Scripts.Player;
using Assets.Scripts.UI.Bar;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.UI
{
    public class BattleHud : MonoBehaviour
    {
        [SerializeField] protected BattleIconAnimator _defenceAnimator;
        [SerializeField] protected HPBar _hpBar;
        [SerializeField] protected SpeedBar _speedBar;
        [SerializeField] protected TMP_Text _defence;
        [SerializeField] protected TMP_Text _damage;
        [SerializeField] protected Area _commonArea;
        [SerializeField] protected Area _defenceArea;
        [SerializeField] protected Area _attackArea;
        [SerializeField] protected Area[] _areas;

        protected IHealth _health;
        protected ISpeed _speed;
        protected AreaDice _dice;

        public AreaDice Dice => _dice;
        public BattleIconAnimator DefenceAnimator => _defenceAnimator;

        public event UnityAction<AreaDice> DiceActivated;

        public void InstantiateDice(AreaDice dicePrefab)
        {
            _dice = Instantiate(dicePrefab);
            DiceActivated?.Invoke(_dice);
            _dice.OnDiceResult += SetBattleArea;
        }

        public virtual void SetBattleArea(DiceFace diceFace) { }

        public void RollDice()
            => _dice.Roll();
                            
        public virtual void UpdateHPBar()
            => _hpBar.SetValue(_health.CurrentHP, _health.MaxHP);              
    }
}
