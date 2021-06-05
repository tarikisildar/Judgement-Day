using System.Collections;
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
            RoundManager.Instance.StartRoundEvent += Initialize;
            GameManager.Instance.GoToMainMenuEvent += Clear;
        }

        private void Clear()
        {
            foreach (var button in buttons)
            {
                button.Clear();
            }
        }
        
        private void Initialize()
        {
            foreach (var button in buttons)
            {
                slots[(int)button.GetComponent<JoystickableInit>().slot] = button;
            }
        }

        public void AddSkill(SkillMain skill)
        {
            StartCoroutine(WaitForAddSkill(skill));
        }

        private IEnumerator WaitForAddSkill(SkillMain skill)
        {
            yield return new WaitForSeconds(Constants.SecondBatch);
            slots[(int)skill.slot].AddSkill(skill);

        }
    }
}