
using System;
using Enums;
using UnityEngine;

namespace Skills
{
    public class SkillMain : MonoBehaviour
    {
        public Skill skillData;
        public SkillSlots slot;

        protected string skillDataName;

        public virtual void Action()
        {
            Debug.Log("Action");
            Instantiate(skillData.projectile);
        }

        protected virtual void Awake()
        {
            skillData = Resources.Load(Constants.SkillDataPath + skillDataName) as Skill;
        }
    }
}