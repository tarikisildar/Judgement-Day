using Skills.Projectiles;
using UnityEngine;

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
            var projectile = Instantiate(skillData.projectile);
            projectile.transform.position = transform.position+ transform.forward*3f;
            projectile.transform.forward = transform.forward;
            projectile.GetComponent<Projectile>().shooter = player;
        }
    }
}