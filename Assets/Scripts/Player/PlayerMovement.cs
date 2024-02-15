using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {        
        [SerializeField] private float _moveSpeed;

        private Vector3 _rightBorder = new Vector3(2.5f, 0, 0);
        private Vector3 _leftBorder = new Vector3(-2.5f, 0, 0);
        private Vector3 _right;
        private Vector3 _left;
        private Vector3 _startPos;
        private Vector3 _defaultPosition = new Vector3(0, 0, 0);
        private bool _isMoving;

        public bool IsMoving => _isMoving;

        public event UnityAction PlayerMoved;

        public void SetDefoultPosition()
        {
            transform.localPosition = _defaultPosition;
            _startPos = _defaultPosition;
        }

        public void CheckButtons(Button right, Button left)
        {
            if (_startPos == _rightBorder) right.interactable = false;
            if (_startPos.x > _leftBorder.x) left.interactable = true;
            if (_startPos == _leftBorder) left.interactable = false;
            if (_startPos.x < _rightBorder.x) right.interactable = true;
        }

        public void MoveRight()
        {
            _right = _startPos += new Vector3(2.5f, 0, 0);
            PlayerMoved?.Invoke();
            StartCoroutine(Move(_right));
        }

        public void MoveLeft()
        {
            _left = _startPos += new Vector3(-2.5f, 0, 0);
            PlayerMoved?.Invoke();
            StartCoroutine(Move(_left));
        }

        public void Push()
        {
            if (transform.localPosition != _leftBorder)
                MoveLeft();
        }

        public void Pull()
        {
            if (transform.localPosition != _rightBorder)
                MoveRight();
        }        

        private IEnumerator Move(Vector3 newPos)
        {
            _isMoving = true;
            while (transform.localPosition != newPos)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, _moveSpeed * Time.deltaTime);
                yield return null;
            }
            _startPos = transform.localPosition;
            _isMoving = false;
        }
    }
}
