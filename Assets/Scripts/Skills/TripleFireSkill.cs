using Skills.Projectiles;
using UnityEngine;
using Photon.Pun;

namespace Skills
{
    public class TripleFireSkill : SkillMain
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Action(GameObject player)
        {
            base.Action(player);
            photonView.RPC("CreateProjectile", RpcTarget.AllViaServer, transform.forward, player.GetComponent<PhotonView>().ViewID);
            photonView.RPC("CreateProjectile", RpcTarget.AllViaServer, transform.forward + transform.right * 0.3f, player.GetComponent<PhotonView>().ViewID);
            photonView.RPC("CreateProjectile", RpcTarget.AllViaServer, transform.forward - transform.right * 0.3f, player.GetComponent<PhotonView>().ViewID);
            //CreateProjectile(transform.forward,player);
            //CreateProjectile(transform.forward+transform.right*0.3f,player);
            //CreateProjectile(transform.forward-transform.right*0.3f,player);

        }

        [PunRPC]
        private void CreateProjectile(Vector3 forward,int playerID)
        {
            if(photonView.IsMine)
            {
                PhotonView pv = PhotonView.Find(playerID);
                var projectile = PhotonNetwork.Instantiate(Constants.SkillPath + skillData.projectile.name, Vector3.zero, Quaternion.identity);
                int procID = projectile.GetComponent<PhotonView>().ViewID;
                photonView.RPC("SetShooter", RpcTarget.All, procID, playerID);
                projectile.transform.position = transform.position + forward * 0.8F;
                projectile.transform.forward = forward;
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