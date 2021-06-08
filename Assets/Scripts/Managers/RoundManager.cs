using System;
using System.Collections;
using System.Linq;
using DefaultNamespace;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class RoundManager : Singleton<RoundManager>, IOnEventCallback
    {
        public int CurrentRound { get; private set; } = 0;
        

        public RoundState roundState = RoundState.NotStarted;

        [SerializeField] private Timer skillSelectionTimer;
        
        public delegate void StartRoundDelegate();
        public event StartRoundDelegate StartRoundEvent;
        
        public delegate void EndRoundDelegate();
        public event EndRoundDelegate EndRoundEvent;

        private float groundBreakTime = 5f;
        private GroundBreak[] grounds;
        
        public const byte StartRoundEventCode = 1;
        public const byte SkillSelectionOverEventCode = 2;
        public const byte EndRoundEventCode = 3;
        public const byte BreakGroundEventCode = 4;


        private void Awake()
        {
            GameManager.Instance.GoToMainMenuEvent += GoToMainMenu;
            GameManager.Instance.StartGameEvent += StartRoundNetworkEvent;
        }
        
        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void StartRoundNetworkEvent()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
                PhotonNetwork.RaiseEvent(StartRoundEventCode, new object[] { }, raiseEventOptions,
                    SendOptions.SendReliable);
            }
        }

        private void SkillSelectionOverNetworkEvent()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
                PhotonNetwork.RaiseEvent(SkillSelectionOverEventCode, new object[] { }, raiseEventOptions,
                    SendOptions.SendReliable);
            }
        }
        private void EndRoundNetworkEvent()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
                PhotonNetwork.RaiseEvent(EndRoundEventCode, new object[] { }, raiseEventOptions,
                    SendOptions.SendReliable);
            }
        }

        private void BreakGroundEvent()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int id = grounds[Random.Range(0, grounds.Length)].GetComponent<PhotonView>().ViewID;
                object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
                PhotonNetwork.RaiseEvent(BreakGroundEventCode, content, raiseEventOptions,
                    SendOptions.SendReliable);
            }
        }
        
        private void GoToMainMenu()
        {
            CurrentRound = 0;
            roundState = RoundState.NotStarted;
        }

        private void Update()
        {
            if (roundState != RoundState.Game || grounds.Length <= 1) return;
            groundBreakTime -= Time.deltaTime;
            if (groundBreakTime <= 0)
            {
                BreakGroundEvent();
                groundBreakTime = Random.Range(4, 7);
            }
        }

        private void BreakGround(int rand)
        {
            var ground = PhotonView.Find(rand).gameObject.GetComponent<GroundBreak>();
            if (ground.broke) return;
            ground.Break();
        }


        private void StartRound()
        {
            InputManager.Instance.DontTakeInput();
            groundBreakTime = 5f;
            
            
            roundState = RoundState.SkillSelection;
            StartRoundEvent?.Invoke();
            StartCoroutine(WaitForSkillSelection());
            skillSelectionTimer.Activate(Constants.SkillSelectionTime);
        }

        private void SkillSelectionOver()
        {
            CanvasManager.Instance.HideSkillSelectionCanvas();
            InputManager.Instance.TakeInput();
            roundState = RoundState.Game;
            StartCoroutine(WaitForRound());
            grounds = GameObject.FindGameObjectsWithTag(Constants.GroundBreakTag)
                .Select(k => k.GetComponent<GroundBreak>()).ToArray();
        }
        
        IEnumerator WaitForSkillSelection()
        {
            yield return new WaitForSeconds(Constants.SkillSelectionTime);
            SkillSelectionOverNetworkEvent();
        }

        IEnumerator WaitForRound()
        {
            var length = Constants.RoundLengths[CurrentRound];
            RoundStartAnim.Instance.Activate(CurrentRound+1,length);
            yield return new WaitForSeconds(length);
            CanvasManager.Instance.HideGameCanvas();
            roundState = RoundState.RoundOver;
            EndRoundNetworkEvent();

        }

        public void EndRound()
        {
            CurrentRound++;
            EndRoundEvent?.Invoke();
            StartCoroutine(CurrentRound < Constants.RoundCount ? WaitForNextRound() : GameManager.Instance.WaitForMainMenu());
        }
        
        IEnumerator WaitForNextRound()
        {
            CanvasManager.Instance.ShowRoundEndingCanvas(CurrentRound,Constants.RoundEndTime);
            yield return new WaitForSeconds(Constants.RoundEndTime);
            CanvasManager.Instance.HideRoundEndingCanvas();
            StartRoundNetworkEvent();
        }
        
        


        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            switch (eventCode)
            {
                case StartRoundEventCode:
                    StartRound();
                    break;
                case SkillSelectionOverEventCode:
                    SkillSelectionOver();
                    break;
                case EndRoundEventCode:
                    EndRound();
                    break;
                case BreakGroundEventCode:
                    object[] data = (object[])photonEvent.CustomData;
                    int rand = (int)data[0];
                    BreakGround(rand);
                    break;
            }
            
        }
    }

    public enum RoundState
    {
        NotStarted, SkillSelection, Game, RoundOver
    }
}