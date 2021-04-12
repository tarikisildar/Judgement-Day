using Managers;
using UnityEngine;

namespace UI
{
    public class MainMenuCanvas : CanvasGeneric
    {
        public void StartGame()
        {
            GameManager.Instance.StartGame();
        }
    }
}