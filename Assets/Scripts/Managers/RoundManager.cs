using System;
using System.Collections;
using System.Linq;
using DefaultNamespace;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class RoundManager : Singleton<RoundManager>
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

        private void Awake()
        {
            GameManager.Instance.GoToMainMenuEvent += GoToMainMenu;
            GameManager.Instance.StartGameEvent += StartRound;
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
            while (groundBreakTime <= 0)
            {
                var ground = grounds[Random.Range(0, grounds.Length)];
                if (ground.broke) continue;
                ground.Break();
                groundBreakTime = Random.Range(4, 7);
            }
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
            SkillSelectionOver();
        }

        IEnumerator WaitForRound()
        {
            var length = Constants.RoundLengths[CurrentRound];
            RoundStartAnim.Instance.Activate(CurrentRound+1,length);
            yield return new WaitForSeconds(length);
            CanvasManager.Instance.HideGameCanvas();
            roundState = RoundState.RoundOver;
            EndRound();

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
            StartRound();
        }

       
    }

    public enum RoundState
    {
        NotStarted, SkillSelection, Game, RoundOver
    }
}