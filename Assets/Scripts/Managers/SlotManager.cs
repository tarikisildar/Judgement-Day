using System.Collections.Generic;
using Enums;
using Skills;
using UI;
using UnityEngine;

namespace Managers
{
    public class SlotManager : Singleton<SlotManager>
    {
        [SerializeField] private JoystickableButton[] buttons;

        private Dictionary<int, JoystickableButton> slots = new Dictionary<int, JoystickableButton>();
        
        private void Awake()
        {
            GameManager.Instance.StartGameEvent += Initialize;
        }
        
        public void Initialize()
        {
            foreach (var button in buttons)
            {
                slots[(int)button.GetComponent<JoystickableInit>().slot] = button;
            }
        }

        public void AddSkill(SkillMain skill)
        {
            slots[(int)skill.slot].AddSkill(skill);
        }
    }
}