using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private GameObject layoutGroup;
        [SerializeField] private GameObject textObject;
        private Player[] players;
        private List<TextMeshProUGUI> texts;

        public void Initialize()
        {
            texts = new List<TextMeshProUGUI>();
            players = PhotonNetwork.PlayerList;
            foreach (Transform child in layoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var player in players)
            {
                texts.Add(Instantiate(textObject,layoutGroup.transform).GetComponent<TextMeshProUGUI>());
                texts[texts.Count - 1].text = player.NickName + " - " + player.GetScore();
            }
            

        }
        private void Update()
        {
            var scores = players.OrderBy(k => k.GetScore()).Reverse().ToList();
            for (int i = 0; i < scores.Count; i++)
            {
                texts[i].text = scores[i].NickName + "-" + scores[i].GetScore();
            }
        }
    }
}