using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : Movement
    {                
        private int _movementAP = 1;

        public bool IsMoving => _isMoving;
        public int MovementAP => _movementAP;

        public event UnityAction PlayerMoved;               

        public void CheckButtons(Button right, Button left)
        {
            if (_startPos == _rightBorder) right.interactable = false;
            if (_startPos.x > _leftBorder.x) left.interactable = true;
            if (_startPos == _leftBorder) left.interactable = false;
            if (_startPos.x < _rightBorder.x) right.interactable = true;
        }

        public override void MoveRight()
        {
            base.MoveRight();
            PlayerMoved?.Invoke();
        }

        public override void MoveLeft()
        {
            base.MoveLeft();
            PlayerMoved?.Invoke();
        }

        public override void Push()
            => MoveLeft();

        public override void Pull()
            => MoveRight();        
    }
}
