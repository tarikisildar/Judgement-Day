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
        private int nextSlotIx = 1;

        public override void Initialize()
        {
            base.Initialize();
            StartCoroutine(WaitForInitialize());

        }

        IEnumerator WaitForInitialize()
        {
            yield return new WaitForSeconds(Constants.FirstBatch);
            var skillPool = Resources.LoadAll<GameObject>(Constants.SkillDataPath);
            controller = SurroundingsManager.Instance.mainPlayer.GetComponent<CharacterController>();

            List<SkillMain> skillScripts = new List<SkillMain>();
            foreach (var skillPoolElement in skillPool)
            {
                var skillMain = skillPoolElement.GetComponent<SkillMain>();
                if(controller.skills.Any(s => s.skillData == skillMain.skillData)) continue;

                skillScripts.Add(skillMain);
                
            }
            skillScripts.Shuffle();

            foreach (var (skillSelection,ix) in skills.WithIndex())
            {
                skillSelection.Initialize(skillScripts[ix]);
            }
            applyButton.interactable = false;
        }
        

        public void SelectSkill()
        {
            foreach (var skillSelection in skills)
            {
                skillSelection.UnSelect();
            }
            var button = EventSystem.current.currentSelectedGameObject.GetComponent<SkillSelection>();
            button.Select();
            selected = button;
            applyButton.interactable = true;
        }

        public void Apply()
        {
            
            selected.SetSkill( controller,(SkillSlots)nextSlotIx);
            nextSlotIx++;
            applyButton.interactable = false;
            CanvasManager.Instance.HideSkillSelectionCanvas();
        }
        
    }
}