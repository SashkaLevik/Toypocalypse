﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class PartsMover : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private AudioSource _moveSound;

        private float _speed = 1200f;

        public void RemovePart(List<Part> parts, Part part, Transform obj)
        {
            parts.Remove(part);
            part.transform.SetParent(_canvas.transform);
            part.SetDisableColor();
            StartCoroutine(Move(part, obj));
            _moveSound.Play();
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
