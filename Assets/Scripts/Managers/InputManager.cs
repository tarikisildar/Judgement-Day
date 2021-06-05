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
            RoundManager.Instance.StartRoundEvent += WaitAndTakeInput;
            RoundManager.Instance.EndRoundEvent += DontTakeInput;
            GameManager.Instance.PlayerDiedEvent += DontTakeInput;
        }

        public void DontTakeInput()
        {
            CanvasManager.Instance.ControlInput(false);
            takeInput = false;
        }
        public void TakeInput()
        {
            CanvasManager.Instance.ControlInput(true);

            takeInput = true;
        }

        private void Update()
        {

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