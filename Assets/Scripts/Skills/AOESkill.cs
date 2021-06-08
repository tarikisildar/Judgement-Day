using System.Collections;
using System.Collections.Generic;
using Controllers;
using Skills.Projectiles;
using UnityEngine;
using Photon.Pun;

namespace Skills
{
    public class AOESkill : SkillMain
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Action(GameObject player)
        {
            base.Action(player);
            photonView.RPC("PhotonAction", RpcTarget.AllViaServer, player.GetComponent<PhotonView>().ViewID);
            //var projectile = Instantiate(skillData.projectile);
        }

        [PunRPC]
        void PhotonAction(int playerID)
        {
            if (photonView.IsMine)
            {
                Debug.Log("test");
                PhotonView pv = PhotonView.Find(playerID);
                var projectile = PhotonNetwork.Instantiate(Constants.SkillPath + skillData.projectile.name, new Vector3(transform.position.x, 0.1f, transform.position.z), Quaternion.identity);
                int procID = projectile.GetComponent<PhotonView>().ViewID;
                photonView.RPC("SetShooter", RpcTarget.All, procID, playerID);
                //projectile.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
                //projectile.GetComponent<Projectile>().shooter = player;
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