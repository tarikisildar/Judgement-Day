using System.Collections;
using System.Collections.Generic;
using Controllers;
using Skills.Projectiles;
using UnityEngine;
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
            var projectile = Instantiate(skillData.projectile,player.transform);
            projectile.transform.position = transform.position;
            StartCoroutine(WaitForTeleport(projectile));
            projectile.GetComponent<Projectile>().shooter = player;

            Destroy(projectile,1.5f);
        }

        IEnumerator WaitForTeleport(GameObject projectile)
        {
            yield return new WaitForSeconds(0.5f);
            projectile.transform.parent = null;
            projectile.transform.position = transform.position + transform.forward*5F;
            transform.parent.position = projectile.transform.position;
        }
    }
}
