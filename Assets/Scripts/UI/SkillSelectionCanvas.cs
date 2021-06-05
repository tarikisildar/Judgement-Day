using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Managers;
using Skills;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CharacterController = Controllers.CharacterController;

namespace UI
{
    public class SkillSelectionCanvas : CanvasGeneric
    {
        [SerializeField] private SkillSelection[] skills;
        [SerializeField] private Button applyButton;
        private CharacterController controller;
        private SkillSelection selected = null;
        private int nextSlotIx = 0;
        private bool locked = false;

        public override void Initialize()
        {
            base.Initialize();
            StartCoroutine(WaitForInitialize());
        }

        IEnumerator WaitForInitialize()
        {
            yield return new WaitForSeconds(Constants.FirstBatch);
            locked = false;
            var skillPool = Resources.LoadAll<GameObject>(Constants.SkillDataPath).ToList();
            controller = SurroundingsManager.Instance.mainPlayer.GetComponent<CharacterController>();
            skillPool.Shuffle();
            
            List<SkillMain> skillScripts = new List<SkillMain>();
            foreach (var roundSkillType in Constants.RoundSkillTypes[RoundManager.Instance.CurrentRound])
            {
                foreach (var skillPoolElement in skillPool)
                {
                    var skillMain = skillPoolElement.GetComponent<SkillMain>();
                    if(roundSkillType != skillMain.skillData.skillType) continue;

                    if(controller.skills.Any(s => s.skillData == skillMain.skillData)) continue;

                    skillScripts.Add(skillMain);

                    skillPool.Remove(skillPoolElement);
                    break;

                }
            }
            

            foreach (var (skillSelection,ix) in skills.WithIndex())
            {
                skillSelection.Initialize(skillScripts[ix]);
            }
            applyButton.interactable = false;
        }
        

        public void SelectSkill()
        {
            if(locked) return;
            foreach (var skillSelection in skills)
            {
                skillSelection.UnSelect();
            }
            var button = EventSystem.current.currentSelectedGameObject.GetComponent<SkillSelection>();
            button.Select();
            selected = button;
            applyButton.interactable = true;
        }

        public void Lock()
        {
            applyButton.interactable = false;
            locked = true;
        }

        public void Apply()
        {
            if (!selected)
            {
                selected = skills[Random.Range(0, skills.Length)];
            }
            selected.SetSkill(controller,(SkillSlots)nextSlotIx);
            nextSlotIx++;
        }
        
    }
}