using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public bool takeInput { get; private set; }= false;

        private void Awake()
        {
            GameManager.Instance.StartGameEvent += WaitAndTakeInput;
            GameManager.Instance.GoToMainMenuEvent += DontTakeInput;
        }

        private void DontTakeInput()
        {
            takeInput = false;
        }
        private void TakeInput()
        {
            takeInput = true;
        }

        private void WaitAndTakeInput()
        {
            StartCoroutine(WaitForTakingInput());
        }

        IEnumerator WaitForTakingInput()
        {
            yield return new WaitForSeconds(Constants.SecondBatch);
            takeInput = true;
        }

    }
}