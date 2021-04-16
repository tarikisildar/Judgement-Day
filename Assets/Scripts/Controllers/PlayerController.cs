using System;
using System.Collections.Generic;
using Enums;
using Managers;
using Skills;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5;
        private Rigidbody rigidbody;
        private List<SkillMain> skills = new List<SkillMain>();

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 input)
        {
            transform.position += new Vector3(input.x,0,input.y) * (speed * Time.deltaTime);
        }

        public void Rotate(Vector2 input)
        {
            transform.LookAt(transform.position + new Vector3(input.x*100,0,input.y*100));
        }

        public void AddSkill( SkillSlots slots, SkillMain skill)
        {
            skill.slot = slots;
            skills.Add(skill);
            SlotManager.Instance.AddSkill(skill);
        }
    }
}