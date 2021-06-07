using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DestroyDelayed : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject,5);
        }
    }
}