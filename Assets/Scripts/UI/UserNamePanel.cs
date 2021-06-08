using System;
using Managers;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UserNamePanel : MonoBehaviour
    {
        [SerializeField] private Button okButton;
        [SerializeField] private TMP_InputField nameField;

        public void Initialize()
        {
            okButton.interactable = false;
        }

        public void NameFieldChanged(String text)
        {
            okButton.interactable = text != "";
        }

        public void SetUserName()
        {
            PlayerPrefs.SetString(Constants.UserNameKey,nameField.text); 
            PhotonNetwork.NickName = nameField.text;
            var mainMenu = transform.parent.GetComponent<MainMenuCanvas>();
            mainMenu.HideUserNameCanvas();
            mainMenu.SetGreeting(GreetingType.NewUser);
        }
    }
}