using System.Collections.Generic;
using Enums;
using Skills;
using UnityEngine;

namespace Controllers
{
    public class CharacterController : MonoBehaviour
    {
        public List<SkillMain> skills { get; private set; } = new List<SkillMain>();

        public virtual void AddSkill(SkillSlots slot,GameObject skill)
        {
            
        }
    }
}