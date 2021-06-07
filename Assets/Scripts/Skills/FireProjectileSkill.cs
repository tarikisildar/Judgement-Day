using Controllers;
using Skills.Projectiles;
using UnityEngine;
using Photon.Pun;

namespace Skills
{
    public class FireProjectileSkill : SkillMain
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
        private void PhotonAction(int playerId)
        {
            if(photonView.IsMine)
            {
                Debug.Log("test");
                PhotonView pv = PhotonView.Find(playerId);
                var projectile = PhotonNetwork.Instantiate(Constants.SkillPath + skillData.projectile.name, Vector3.zero, Quaternion.identity);
                int procID = projectile.GetComponent<PhotonView>().ViewID;
                photonView.RPC("SetShooter", RpcTarget.All, procID, playerId);
                projectile.transform.position = transform.position + transform.forward * 0.8F;
                projectile.transform.forward = transform.forward;
                //projectile.GetComponent<Projectile>().shooter = pv.gameObject;
            }
        }

        [PunRPC]
        private void SetShooter(int procID, int playerID)
        {
            PhotonView proc = PhotonView.Find(procID);
            PhotonView player = PhotonView.Find(playerID);
            proc.gameObject.GetComponent<Projectile>().shooter = player.gameObject;
        }
    }
}