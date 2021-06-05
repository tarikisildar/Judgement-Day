using System;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChangeCharacterCanvas : CanvasGeneric
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        private Characters chosenCharacter;

        public override void Initialize()
        {
            base.Initialize();
            chosenCharacter = (Characters)PlayerPrefs.GetInt(Constants.PlayerKey, 0);
            ChangeCharacter();
            CheckInteractable();
        }

        public void Left()
        {
            chosenCharacter = (Characters) ((int) chosenCharacter - 1);
            ChangeCharacter();

        }

        public void Right()
        {
            chosenCharacter = (Characters) ((int) chosenCharacter + 1);
            ChangeCharacter();
        }

        private void CheckInteractable()
        {
            int charCount = Enum.GetNames(typeof(Characters)).Length;

            if ((int) chosenCharacter == 0)
            {
                leftButton.interactable = false;
            }
            else
            {
                leftButton.interactable = true;
            }
            
            if ((int) chosenCharacter == charCount-1)
            {
                rightButton.interactable = false;
            }
            else
            {
                rightButton.interactable = true;
            }
        }
        private void ChangeCharacter()
        {
            SurroundingsManager.Instance.CreatePlayerForChoosing(chosenCharacter);
            CheckInteractable();

        }

        public void Save()
        {
            PlayerPrefs.SetInt(Constants.PlayerKey,(int)chosenCharacter);
            GoBack();
        }

        public void GoBack()
        {
            CanvasManager.Instance.HideCharacterChooseCanvas();
            GameManager.Instance.GoToMainMenu();
            SurroundingsManager.Instance.DestroyPlayerAfterChoosing();
        }
    }
}