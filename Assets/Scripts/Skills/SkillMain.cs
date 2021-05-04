
using System;
using Controllers;
using Enums;
using UnityEngine;

namespace Skills
{
    public class SkillMain : MonoBehaviour
    {
        public Skill skillData;
        public SkillSlots slot;


        public virtual void Action(GameObject player)
        {
            Debug.Log("Action");
        }

        protected virtual void Awake()
        {
        }

        public void LoadSkillData()
        {

        }
    }
}