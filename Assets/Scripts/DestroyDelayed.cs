using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace
{
    public class DestroyDelayed : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(DestroyDelay(5));
        }

        IEnumerator DestroyDelay(float time)
        {
            yield return new WaitForSeconds(time);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}