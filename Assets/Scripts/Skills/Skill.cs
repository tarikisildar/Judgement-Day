using Enums;
using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill",order = 1)]
    public class Skill : ScriptableObject
    {
        public bool hasDirection;
        public GameObject projectile;
        public float coolDownTime;
    }
}