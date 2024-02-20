using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] protected float _moveSpeed;

        protected bool _isMoving = false;
        protected Vector3 _right;
        protected Vector3 _left;
        protected Vector3 _startPos;
        protected Vector3 _rightBorder = new Vector3(2.5f, 0, 0);
        protected Vector3 _leftBorder = new Vector3(-2.5f, 0, 0);
        protected Vector3 _defaultPosition = new Vector3(0, 0, 0);

        public void SetDefoultPosition()
        {
            transform.localPosition = _defaultPosition;
            _startPos = _defaultPosition;
        }

        public virtual void MoveRight()
        {
            if (CanMove(_rightBorder))
            {
                _right = _startPos += new Vector3(2.5f, 0, 0);
                StartCoroutine(Move(_right));
            }
        }

        public virtual void MoveLeft()
        {
            if (CanMove(_leftBorder))
            {
                _left = _startPos += new Vector3(-2.5f, 0, 0);
                StartCoroutine(Move(_left));
            }
        }

        public virtual void Push() { }            

        public virtual void Pull() { }            

        private bool CanMove(Vector3 border)
        {
            if (transform.localPosition != border)
                return true;

            return false;
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
