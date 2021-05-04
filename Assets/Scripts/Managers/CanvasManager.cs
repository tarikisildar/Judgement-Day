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


        private void Awake()
        {
            GameManager.Instance.GoToMainMenuEvent += HideGameCanvas;
            GameManager.Instance.GoToMainMenuEvent += HideLevelFinishCanvas;
            GameManager.Instance.GoToMainMenuEvent += HidePauseMenuCanvas;
            GameManager.Instance.GoToMainMenuEvent += ShowMainMenuCanvas;

            GameManager.Instance.StartGameEvent += HideMainMenuCanvas;
            GameManager.Instance.StartGameEvent += HideLevelFinishCanvas;
            GameManager.Instance.StartGameEvent += HidePauseMenuCanvas;
            GameManager.Instance.StartGameEvent += ShowGameCanvas;
            GameManager.Instance.StartGameEvent += ShowSkillSelectionCanvas;
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
            mainMenuCanvas.GetComponent<FadeHandler>().FadeIn(0.5F);
            mainMenuCanvas.GetComponent<MainMenuCanvas>().Initialize();
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
            skillSelectionCanvas.GetComponent<FadeHandler>().FadeOut();
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