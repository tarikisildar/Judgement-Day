using Skills.Projectiles;
using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace Skills
{
    public class HealingSkill : SkillMain
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
            PhotonView pv = PhotonView.Find(playerID);
            var projectile = PhotonNetwork.Instantiate(Constants.SkillPath + skillData.projectile.name, transform.position, transform.rotation);
            int procID = projectile.GetComponent<PhotonView>().ViewID;
            photonView.RPC("SetParent", RpcTarget.All, procID, playerID);
            var projectileSc = skillData.projectile.GetComponent<Projectile>();
            pv.gameObject.GetComponent<CharacterStats>().health +=
                ((Healing)projectileSc).power;
            photonView.RPC("SetShooter", RpcTarget.All, procID, playerID);
            //projectileSc.shooter = pv.gameObject;
            StartCoroutine(DestroyThis(procID));
        }

        [PunRPC]
        void SetParent(int procID, int playerID)
        {
            PhotonView proc = PhotonView.Find(procID);
            PhotonView player = PhotonView.Find(playerID);
            proc.transform.SetParent(player.transform);
        }

        [PunRPC]
        void SetShooter(int procID, int playerID)
        {
            PhotonView proc = PhotonView.Find(procID);
            PhotonView player = PhotonView.Find(playerID);
            proc.gameObject.GetComponent<Projectile>().shooter = player.gameObject;
        }

        IEnumerator DestroyThis(int procID)
        {
            yield return new WaitForSeconds(1.5f);
            PhotonNetwork.Destroy(PhotonView.Find(procID).gameObject);
        }
    }
}