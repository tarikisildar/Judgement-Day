using System.Collections.Generic;
using Enums;
using Skills;
using UnityEngine;
using Photon.Pun;

namespace Controllers
{
    public class CharacterController : MonoBehaviourPun
    {
        public List<SkillMain> skills { get; private set; } = new List<SkillMain>();

        public virtual void AddSkill(SkillSlots slot,GameObject skill)
        {
            
        }
    }
}