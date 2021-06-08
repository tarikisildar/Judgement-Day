using System;
using UI;
using UnityEngine;

namespace Managers
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [SerializeField] private Canvas gameCanvas;
        [SerializeField] private Canvas mainMenuCanvas;
        [SerializeField] private Canvas levelFinishCanvas;
        [SerializeField] private Canvas pauseMenuCanvas;
        [SerializeField] private Canvas skillSelectionCanvas;
        [SerializeField] private Canvas roundEndingCanvas;
        [SerializeField] private Canvas characterChooseCanvas;
        [SerializeField] private Canvas popUpCanvas;


        private void Awake()
        {
            GameManager.Instance.GoToMainMenuEvent += HideGameCanvas;
            GameManager.Instance.GoToMainMenuEvent += HideLevelFinishCanvas;
            GameManager.Instance.GoToMainMenuEvent += ShowMainMenuCanvas;

            GameManager.Instance.StartGameEvent += HideMainMenuCanvas;
            GameManager.Instance.StartGameEvent += HideLevelFinishCanvas;
            
            
            RoundManager.Instance.StartRoundEvent+= ShowGameCanvas;
            RoundManager.Instance.StartRoundEvent += ShowSkillSelectionCanvas;
            RoundManager.Instance.StartRoundEvent += HideLevelFinishCanvas;
            
            RoundManager.Instance.EndRoundEvent += HideGameCanvas;
            RoundManager.Instance.EndRoundEvent += ShowLevelFinishCanvas;
            
            ShowPopUpCanvas();
            GetPopUpCanvas().ShowConnectingPopUp();
        }

        public GameCanvas GetGameCanvas()
        {
            return gameCanvas.GetComponent<GameCanvas>();
        }
        
        
        public void ShowGameCanvas()
        {
            gameCanvas.GetComponent<FadeHandler>().FadeIn(0.5F);
            gameCanvas.GetComponent<GameCanvas>().Initialize();
        }


        public void HideGameCanvas()
        {
            gameCanvas.GetComponent<FadeHandler>().FadeOut();
        }
        
        public MainMenuCanvas GetMainMenuCanvas()
        {
            return mainMenuCanvas.GetComponent<MainMenuCanvas>();
        }
        
        
        public void ShowMainMenuCanvas()
        {
            var mmCanvas = mainMenuCanvas.GetComponent<MainMenuCanvas>();
            if(!PlayerPrefs.HasKey(Constants.UserNameKey)) mmCanvas.ShowUserNameCanvas(); 
            mainMenuCanvas.GetComponent<FadeHandler>().FadeIn(0.5F);
            mmCanvas.Initialize();
        }


        public void HideMainMenuCanvas()
        {
            mainMenuCanvas.GetComponent<FadeHandler>().FadeOut();
        }
        
        public PauseMenuCanvas GetPauseMenuCanvas()
        {
            return pauseMenuCanvas.GetComponent<PauseMenuCanvas>();
        }
        
        
        public void ShowPauseMenuCanvas()
        {
            pauseMenuCanvas.GetComponent<FadeHandler>().FadeIn(0.5F);
            pauseMenuCanvas.GetComponent<PauseMenuCanvas>().Initialize();
        }


        public void HidePauseMenuCanvas()
        {
            pauseMenuCanvas.GetComponent<FadeHandler>().FadeOut();
        }
        
        public LevelFinishCanvas GetLevelFinishCanvas()
        {
            return levelFinishCanvas.GetComponent<LevelFinishCanvas>();
        }
        
        
        public void ShowLevelFinishCanvas()
        {
            levelFinishCanvas.GetComponent<FadeHandler>().FadeIn(0.5F);
            levelFinishCanvas.GetComponent<LevelFinishCanvas>().Initialize();
        }


        public void HideLevelFinishCanvas()
        {
            levelFinishCanvas.GetComponent<FadeHandler>().FadeOut();
        }
        
        public SkillSelectionCanvas GetSkillSelectionCanvas()
        {
            return skillSelectionCanvas.GetComponent<SkillSelectionCanvas>();
        }
        
        
        public void ShowSkillSelectionCanvas()
        {
            skillSelectionCanvas.GetComponent<FadeHandler>().FadeIn(0.5F);
            skillSelectionCanvas.GetComponent<SkillSelectionCanvas>().Initialize();
        }


        public void HideSkillSelectionCanvas()
        {
            skillSelectionCanvas.GetComponent<SkillSelectionCanvas>().Apply();
            skillSelectionCanvas.GetComponent<FadeHandler>().FadeOut();
        }

        public void ControlInput(bool on)
        {
            gameCanvas.GetComponent<GameCanvas>().TurnInput(on);
        }

        public void ShowDiedCanvas(float respawnTime)
        {
            gameCanvas.GetComponent<GameCanvas>().YouDiedActivate(respawnTime);
        }
        public void HideDiedCanvas()
        {
            gameCanvas.GetComponent<GameCanvas>().YouDiedDeactivate();
        }

        public void ShowRoundEndingCanvas(int round, float timeToNextStage)
        {
            roundEndingCanvas.GetComponent<RoundEndingUI>().Initialize(round,timeToNextStage);
        }

        public void HideRoundEndingCanvas()
        {
            roundEndingCanvas.GetComponent<FadeHandler>().FadeOut();
        }

        public void ShowCharacterChooseCanvas()
        {
            characterChooseCanvas.GetComponent<ChangeCharacterCanvas>().Initialize();
            characterChooseCanvas.GetComponent<FadeHandler>().FadeIn(1f);
        }

        public void HideCharacterChooseCanvas()
        {
            characterChooseCanvas.GetComponent<FadeHandler>().FadeOut();

        }

        public PopupCanvas GetPopUpCanvas()
        {
            return popUpCanvas.GetComponent<PopupCanvas>();
        }
        
        public void ShowPopUpCanvas()
        {
            popUpCanvas.GetComponent<FadeHandler>().FadeIn(0.5f);
            popUpCanvas.GetComponent<PopupCanvas>().Initialize();
        }
        public void HidePopUpCanvas()
        {
            popUpCanvas.GetComponent<FadeHandler>().FadeOut();
        }

        public void HideAllCanvases()
        {
            HideGameCanvas();
            HideLevelFinishCanvas();
            HideMainMenuCanvas();
            HidePauseMenuCanvas();
        }
        
        
    }
}