using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FallCheck : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PlayerKey))
            {
                other.GetComponent<CharacterStats>().TakeDamage(1000);
            }
        }
    }
}