using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class PlayerMovement : MonoBehaviour
    {        
        [SerializeField] private float _moveSpeed;

        private float rightBorder = 2.5f;
        private float _leftBorder = -2.5f;
        private float _playerPos;
        private bool _isMoving;
        private Vector3 _right;
        private Vector3 _left;
        private Vector3 _startPos;
        private Vector3 _defaultPosition = new Vector3(0, 0, 0);

        public bool IsMoving => _isMoving;        

        public void SetDefoultPosition()
        {
            transform.localPosition = _defaultPosition;
            _playerPos = 0;
            _startPos = _defaultPosition;
        }

        public void MoveRight(Button right, Button left)
        {
            _playerPos += 2.5f;
            if (_playerPos == rightBorder) right.interactable = false;
            if (_playerPos > _leftBorder) left.interactable = true;
            _right = _startPos += new Vector3(2.5f, 0, 0);
            StartCoroutine(Move(_right));            
        }

        public void MoveLeft(Button right, Button left)
        {
            _playerPos -= 2.5f;
            if (_playerPos == _leftBorder) left.interactable = false;
            if (_playerPos < rightBorder) right.interactable = true;
            _left = _startPos += new Vector3(-2.5f, 0, 0);
            StartCoroutine(Move(_left));
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
