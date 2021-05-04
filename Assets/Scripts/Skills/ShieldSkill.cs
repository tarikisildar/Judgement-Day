using System.Collections;
using System.Collections.Generic;
using Controllers;
using Skills.Projectiles;
using UnityEngine;

namespace Skills
{
    public class ShieldSkill : SkillMain
    {
        protected override void Awake()
            {
                base.Awake();
            }

            public override void Action(GameObject player)
            {
                base.Action(player);
                var projectile = Instantiate(skillData.projectile, transform);
                projectile.transform.localScale = Vector3.Scale(projectile.transform.localScale, 
                                                                new Vector3(1/transform.localScale.x, 1/transform.localScale.y, 1/transform.localScale.z));
                projectile.GetComponent<Projectile>().shooter = player;
            }
    }
}