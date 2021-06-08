using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class ConnectingPopUp : MonoBehaviour
    {
        private float time;
        public void Initialize()
        {
            time = Time.time;
        }

        private void Update()
        {
            if (IsConnected())
            {
                transform.parent.GetComponent<PopupCanvas>().HideConnectingPopUp();
            }
        }

        private bool IsConnected()
        {
            return Time.time - time > 3; //TODO: connect photon
        }
        
        
    }
}