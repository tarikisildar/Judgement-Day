using System;
using Enums;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void GoToMainMenuDelegate();
        public event GoToMainMenuDelegate GoToMainMenuEvent;
        
        public delegate void StartGameDelegate();
        public event StartGameDelegate StartGameEvent;
        
        
        public GameState gameState;

        private void Start()
        {
            GoToMainMenu();
        }

        public void GoToMainMenu()
        {
            gameState = GameState.MainMenu;
            GoToMainMenuEvent?.Invoke();
            
        }

        public void StartGame()
        {
            gameState = GameState.Game;
            StartGameEvent?.Invoke();
        }
    }
}