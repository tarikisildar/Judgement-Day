using Skills.Projectiles;
using UnityEngine;

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
            var projectile = Instantiate(skillData.projectile, transform);
            var projectileSc = skillData.projectile.GetComponent<Projectile>();
            player.GetComponent<CharacterStats>().health +=
                ((Healing) projectileSc).power;
            projectileSc.shooter = player;
            Destroy(projectile,1.5f);

        }
    }
}