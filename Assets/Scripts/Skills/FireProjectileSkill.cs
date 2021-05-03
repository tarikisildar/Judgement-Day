using Controllers;
using Skills.Projectiles;
using UnityEngine;

namespace Skills
{
    public class FireProjectileSkill : SkillMain
    {
        protected override void Awake()
        {
            skillDataName = "FireProjectile";
            base.Awake();
        }

        public override void Action(GameObject player)
        {
            base.Action(player);
            var projectile = Instantiate(skillData.projectile);
            projectile.transform.position = transform.position + transform.forward*0.8F;
            projectile.transform.forward = transform.forward;
            projectile.GetComponent<Projectile>().shooter = player;

        }
    }
}