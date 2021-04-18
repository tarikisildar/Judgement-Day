using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class AOESkill : SkillMain
    {
        protected override void Awake()
            {
                skillDataName = "AOE";
                base.Awake();
            }

            public override void Action()
            {
                base.Action();
                var projectile = Instantiate(skillData.projectile);
                projectile.transform.position = new Vector3(transform.position.x, 0.01f, transform.position.y);
            }
    }
}