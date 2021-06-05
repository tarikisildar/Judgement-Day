using Enums;
using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill",order = 1)]
    public class Skill : ScriptableObject
    {
        public string skillName;
        public SkillMain skillScript;
        public bool hasDirection;
        public GameObject projectile;
        public float coolDownTime;
        public Sprite icon;
        public SkillType skillType;
    }
}