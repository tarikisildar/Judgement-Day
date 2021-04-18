using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class ShieldSkill : SkillMain
    {
        protected override void Awake()
            {
                skillDataName = "Shield";
                base.Awake();
            }

            public override void Action()
            {
                base.Action();
                var projectile = Instantiate(skillData.projectile, transform);
                projectile.transform.localScale = Vector3.Scale(projectile.transform.localScale, 
                                                                new Vector3(1/transform.localScale.x, 1/transform.localScale.y, 1/transform.localScale.z));
            }
    }
}