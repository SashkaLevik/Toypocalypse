using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private Vector3 _right;
        private Vector3 _left;
        private Vector3 _startPos;
        private Vector3 _rightBorder = new Vector3(2.5f, 0, 0);
        private Vector3 _leftBorder = new Vector3(-2.5f, 0, 0);

        private IEnumerator Move(Vector3 newPos)
        {
            while (transform.localPosition != newPos)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, _moveSpeed * Time.deltaTime);
                yield return null;
            }
            _startPos = transform.localPosition;
        }

        public void MoveRight()
        {
            _right = _startPos += new Vector3(2.5f, 0, 0);
            StartCoroutine(Move(_right));
        }

        public void MoveLeft()
        {
            _left = _startPos += new Vector3(-2.5f, 0, 0);
            StartCoroutine(Move(_left));
        }

        public void Push()
        {
            if (transform.localPosition != _rightBorder)
                MoveRight();
        }

        public void Pull()
        {
            if (transform.localPosition != _leftBorder)
                MoveLeft();
        }
    }
}
