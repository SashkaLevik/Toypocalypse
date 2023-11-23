using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class PartsMover : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private float _speed = 12f;

        public void RemovePart(List<Part> parts, Part part, Transform obj)
        {
            parts.Remove(part);
            part.transform.SetParent(_canvas.transform);
            part.SetDisableColor();
            StartCoroutine(Move(part, obj));
        }

        private IEnumerator Move(Part part, Transform obj)
        {
            yield return new WaitForSeconds(0.1f);

            while (part.transform.position != obj.transform.position)
            {
                part.transform.position = Vector3.MoveTowards(part.transform.position, obj.transform.position, Time.deltaTime * _speed);
                yield return null;
            }

            part.transform.SetParent(obj.transform);
        }
    }
}
