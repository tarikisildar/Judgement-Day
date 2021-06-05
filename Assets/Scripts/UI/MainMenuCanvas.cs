using System;
using Enums;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MainMenuCanvas : CanvasGeneric
    {
        [SerializeField] private GameObject userNameCanvas;
        [SerializeField] private TextMeshProUGUI greetingText;

        private void Awake()
        {
            if(!PlayerPrefs.HasKey(Constants.UserNameKey))
            {
                SetGreeting(GreetingType.Stranger);
            }
            else
            {
                SetGreeting(GreetingType.WelcomeBack);
            }
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame(); //TODO: Matchmaking interface
        }

        public void ChangeMap()
        {
            Maps map = (Maps)PlayerPrefs.GetInt(Constants.MapKey, 0);
            int MapCount = Enum.GetNames(typeof(Maps)).Length;
            Maps newMap = (Maps) (((int)map + 1)% MapCount);
            PlayerPrefs.SetInt(Constants.MapKey,(int)newMap);
            SurroundingsManager.Instance.LoadEnvironment(newMap);
            
        }

        public void ChangeCharacterMenu()
        {
            CanvasManager.Instance.HideMainMenuCanvas();
            CanvasManager.Instance.ShowCharacterChooseCanvas();
        }

        public void Options()
        {
            CanvasManager.Instance.HideMainMenuCanvas();
            CanvasManager.Instance.ShowPauseMenuCanvas();
        }

        public void ShowUserNameCanvas()
        {
            userNameCanvas.GetComponent<FadeHandler>().FadeIn(0.5f);
            userNameCanvas.GetComponent<UserNamePanel>().Initialize();
        }

        public void HideUserNameCanvas()
        {
            userNameCanvas.GetComponent<FadeHandler>().FadeOut();
        }


        public void SetGreeting(GreetingType greetingType)
        {
            switch (greetingType)
            {
                case GreetingType.Stranger:
                    greetingText.text = Constants.StrangerGreeting;
                    break;
                case GreetingType.NewUser:
                    greetingText.text = Constants.NewUserGreeting + PlayerPrefs.GetString(Constants.UserNameKey);
                    break;
                case GreetingType.WelcomeBack:
                    greetingText.text = Constants.WelcomeBackGreeting + PlayerPrefs.GetString(Constants.UserNameKey);
                    break;
            }
        }
    }

    public enum GreetingType
    {
        Stranger,NewUser,WelcomeBack
    }
}