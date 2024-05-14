using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

        public event UnityAction UnitMoved;

        public void SetDefoultPosition()
        {
            StartCoroutine(Move(_defaultPosition));
            UnitMoved?.Invoke();
        }

        public virtual void MoveRight()
        {
            StartCoroutine(Move(_rightBorder));

            UnitMoved?.Invoke();
        }

        public virtual void MoveLeft()
        {
            StartCoroutine(Move(_leftBorder));
            UnitMoved?.Invoke();

            //if (CanMove(_leftBorder))
            //{
            //    _left = _startPos += new Vector3(-2.5f, 0, 0);
            //    StartCoroutine(Move(_left));
            //}
        }

        public virtual void PushInAttack() { }            

        public virtual void PushInDefence() { }            

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
            //_startPos = transform.localPosition;
            _isMoving = false;
        }
    }
}
