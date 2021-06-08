using System;
using Managers;
using UnityEngine;

namespace UI
{
    public class PopupCanvas : CanvasGeneric
    {
        [SerializeField] private GameObject connecting;
        [SerializeField] private GameObject searchRoom;
        public override void Initialize()
        {
            base.Initialize();
        }



        public void ShowConnectingPopUp()
        {
            connecting.GetComponent<FadeHandler>().FadeIn(0.5f);
        }

        public void HideConnectingPopUp()
        {
            connecting.GetComponent<FadeHandler>().FadeOut();
            GetComponent<FadeHandler>().FadeOut();
        }

        public void ShowSearchRoomPopUp()
        {
            searchRoom.GetComponent<FadeHandler>().FadeIn(0.5f);

        }

        public void HideSearchRoomPopUp()
        {
            searchRoom.GetComponent<FadeHandler>().FadeOut();
            GetComponent<FadeHandler>().FadeOut();
        }
        public SearchRoomPopup GetSearchRoomPopUp()
        {
            return searchRoom.GetComponent<SearchRoomPopup>();

        }

    }
}