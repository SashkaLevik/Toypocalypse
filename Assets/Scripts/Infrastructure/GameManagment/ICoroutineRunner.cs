using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameManagment
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}