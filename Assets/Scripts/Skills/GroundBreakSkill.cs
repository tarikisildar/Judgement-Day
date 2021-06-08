using Skills.Projectiles;
using UnityEngine;
using Photon.Pun;

namespace Skills
{
    public class GroundBreakSkill : SkillMain
    {
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
                photonView.RPC("SetTransform", RpcTarget.AllViaServer, procID);
                photonView.RPC("SetParent", RpcTarget.All, procID);
                photonView.RPC("SetShooter", RpcTarget.All, procID, playerID);
            }
        }

        [PunRPC]
        void SetTransform(int procID)
        {
            var projectile = PhotonView.Find(procID).gameObject;
            projectile.transform.position = transform.position + transform.forward * 3f;
            projectile.transform.forward = transform.forward;
        }

        [PunRPC]
        void SetParent(int procID)
        {
            PhotonView proc = PhotonView.Find(procID);
            proc.transform.SetParent(null);
        }

        [PunRPC]
        void SetShooter(int procID, int playerID)
        {
            PhotonView proc = PhotonView.Find(procID);
            PhotonView player = PhotonView.Find(playerID);
            proc.gameObject.GetComponent<Projectile>().shooter = player.gameObject;
        }
    }
}