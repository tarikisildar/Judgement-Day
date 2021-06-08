using System;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SearchRoomPopup : MonoBehaviour
    {
        private float time;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private GameObject layoutGroup;
        [SerializeField] private GameObject userText;
        public void Initialize()
        {
            time = Time.time;
        }

        private void Update()
        {
            if (IsConnected(10))
            {
                GameManager.Instance.StartGame();
                transform.parent.GetComponent<PopupCanvas>().HideSearchRoomPopUp();
                time = Time.time;
            }
            else if (IsConnected(7))
            {
                Starting();
            }
            else if (IsConnected(3))
            {
                WaitingPlayers();
            }
            else
            {
                Searching();
            }
        }

        private bool IsConnected(float t)
        {
            return Time.time - time > t; //TODO: connect photon
        }

        public void Searching()
        {
            title.GetComponent<ThreeDots>().SetOriginalText("Searching For A Game");
        }

        public void WaitingPlayers()
        {
            title.GetComponent<ThreeDots>().SetOriginalText("Waiting Other Players");
            SetUserNames();
        }

        private List<String> GetUserNames()
        {
            var userNames = new List<String>();
            userNames.Add("Ahmet");
            userNames.Add("Mehmet"); //TODO
            userNames.Add("Mahmut");
            return userNames;
        }

        private void SetUserNames()
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
        }
    }
}