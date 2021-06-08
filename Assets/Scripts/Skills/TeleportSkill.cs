using System.Collections;
using System.Collections.Generic;
using Controllers;
using Skills.Projectiles;
using UnityEngine;
using Photon.Pun;

namespace Skills 
{
    public class TeleportSkill : SkillMain
    {
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Action(GameObject player)
        {
            base.Action(player);
            photonView.RPC("PhotonAction", RpcTarget.AllViaServer, player.GetComponent<PhotonView>().ViewID);
        }

        [PunRPC]
        void PhotonAction(int playerID)
        {
            if (photonView.IsMine)
            {
                PhotonView pv = PhotonView.Find(playerID);
                var projectile = PhotonNetwork.Instantiate(Constants.SkillPath + skillData.projectile.name, pv.transform.position, pv.transform.rotation);
                int procID = projectile.GetComponent<PhotonView>().ViewID;
                photonView.RPC("SetParent", RpcTarget.All, procID, playerID);
                StartCoroutine(WaitForTeleport(procID));
                photonView.RPC("SetShooter", RpcTarget.All, procID, playerID);
                StartCoroutine(DestroyThis(procID));
            }
        }

        [PunRPC]
        void SetShooter(int procID, int playerID)
        {
            PhotonView proc = PhotonView.Find(procID);
            PhotonView player = PhotonView.Find(playerID);
            proc.gameObject.GetComponent<Projectile>().shooter = player.gameObject;
        }

        [PunRPC]
        void SetParent(int procID, int playerID)
        {
            PhotonView proc = PhotonView.Find(procID);
            PhotonView player = PhotonView.Find(playerID);
            proc.transform.SetParent(player.transform);
            proc.transform.position = transform.position;
        }

        IEnumerator WaitForTeleport(int procID)
        {
            yield return new WaitForSeconds(0.5f);
            var projectile = PhotonView.Find(procID).gameObject;
            projectile.transform.parent = null;
            projectile.transform.position = transform.position + transform.forward*5F;
            transform.parent.position = projectile.transform.position;
        }

        IEnumerator DestroyThis(int procID)
        {
            yield return new WaitForSeconds(1.5f);
            PhotonNetwork.Destroy(PhotonView.Find(procID).gameObject);
        }
    }
}
