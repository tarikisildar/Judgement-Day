using System;
using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace
{
    public class FallCheck : MonoBehaviourPun
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PlayerKey) )
            {
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamageFromClient", RpcTarget.All, 1000,0, false);
            }
        }
    }
}