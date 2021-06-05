using System;
using Enums;
using Managers;
using Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Controllers.CharacterController;

namespace UI
{
    public class SkillSelection : MonoBehaviour
    {
        public GameObject selection;
        public Image icon;
        public TextMeshProUGUI text;
        private SkillMain skillMain;

        public void Initialize(SkillMain skill)
        {
            icon.sprite = skill.skillData.icon;
            text.text = skill.skillData.skillName;
            skillMain = skill;
        }
        
        
        public void Select()
        {
            selection.SetActive(true);
            text.color = Constants.Gold;
            
        }

        public void UnSelect()
        {
            selection.SetActive(false);
            text.color = Color.white;
        }
        
        

        public void SetSkill(CharacterController controller,SkillSlots slot)
        {
            SurroundingsManager.Instance.mainPlayerSkills.Add(skillMain);
            controller.AddSkill(slot,skillMain.gameObject);
        }
    }
}