using System;
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void GoToMainMenuDelegate();
        public event GoToMainMenuDelegate GoToMainMenuEvent;
        
        public delegate void StartGameDelegate();
        public event StartGameDelegate StartGameEvent;
        
        public delegate void PlayerDieDelegate();
        public event PlayerDieDelegate PlayerDiedEvent;
        
        public delegate void PlayerSpawnDelegate();
        public event PlayerSpawnDelegate PlayerSpawnedEvent;
        

        
        public GameState gameState;

        private void Start()
        {
            AudioListener.pause = PlayerPrefs.GetInt(Constants.SoundKey, 1) == 0;
            NetworkManager.Connect();
            GoToMainMenu();
        }
        IEnumerator AfterStart()
        {
            yield return new WaitForSeconds(2f);
        }
        public void GoToMainMenu()
        {
            gameState = GameState.MainMenu;
            GoToMainMenuEvent?.Invoke();
            
        }

        public void PlayerDied()
        {
            PlayerDiedEvent?.Invoke();
        }

        public void PlayerSpawned()
        {
            PlayerSpawnedEvent?.Invoke();
        }
        

        public void StartGame()
        {

            gameState = GameState.Game; 
            StartGameEvent?.Invoke();
            PhotonNetwork.LocalPlayer.SetScore(0);
            //RoundManager.Instance.StartRound();

        }

        public IEnumerator WaitForMainMenu()
        {
            CanvasManager.Instance.ShowRoundEndingCanvas(RoundManager.Instance.CurrentRound,Constants.GameEndTime);
            gameState = GameState.GameFinish;
            yield return new WaitForSeconds(Constants.GameEndTime);
            CanvasManager.Instance.HideRoundEndingCanvas();
            NetworkManager.Disconnect();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}