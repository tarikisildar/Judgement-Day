using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skills 
{
    public class TeleportSkill : SkillMain
    {
        // Start is called before the first frame update
        protected override void Awake()
        {
            skillDataName = "Teleport";
            base.Awake();
        }

        public override void Action()
        {
            base.Action();
            var projectile = Instantiate(skillData.projectile);
            projectile.transform.position = transform.position + transform.forward*1.5F;
            transform.position = projectile.transform.position;
            Destroy(projectile);
        }
    }
}
