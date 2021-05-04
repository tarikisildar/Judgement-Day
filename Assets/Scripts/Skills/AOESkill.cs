using System.Collections;
using System.Collections.Generic;
using Controllers;
using Skills.Projectiles;
using UnityEngine;

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
                var projectile = Instantiate(skillData.projectile);
                projectile.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
                projectile.GetComponent<Projectile>().shooter = player;

            }
    }
}