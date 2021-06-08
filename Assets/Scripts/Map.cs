using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace
{
    public class Map : MonoBehaviourPun
    {
        public Material skyBox;
        [PunRPC]
        private void SetParent(int id,int universeTransformId)
        {
            var envId = PhotonView.Find(id);
            var universeTransform = PhotonView.Find(universeTransformId);
            envId.transform.SetParent(universeTransform.transform);
        }
    }
}