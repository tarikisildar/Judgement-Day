using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SearchRoomPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private GameObject layoutGroup;
        [SerializeField] private GameObject userText;
        [SerializeField] private Timer timer;
        [SerializeField] private GameObject exitButton;


        public void Searching()
        {
            title.GetComponent<ThreeDots>().SetOriginalText("Searching For A Game");
            exitButton.SetActive(true);
        }

        public void WaitingPlayers()
        {
            title.GetComponent<ThreeDots>().SetOriginalText("Waiting Other Players");
            SetUserNames();
        }

        private List<String> GetUserNames()
        {
            var players= PhotonNetwork.PlayerList.Select(k => k.NickName).ToList();
            return players;
        }

        public void SetUserNames()
        {
            foreach (Transform child in layoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
            List<String> users = GetUserNames();
            for (int i = 0; i < users.Count; i++)
            {
                var userTextObj = Instantiate(userText, layoutGroup.transform);
                userTextObj.GetComponent<TextMeshProUGUI>().text = users[i] + " - Ready";
                
            }
            
        }

        public void Starting()
        {
            title.GetComponent<ThreeDots>().SetOriginalText("Starting");
            exitButton.SetActive(false);
            timer.gameObject.SetActive(true);
            timer.Activate(4);
        }

        public void CancelSearch()
        {
            NetworkManager.LeaveRoom();
            transform.parent.GetComponent<PopupCanvas>().HideSearchRoomPopUp();
        }
    }
}