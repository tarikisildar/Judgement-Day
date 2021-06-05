using System;
using Managers;
using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class PauseMenuCanvas : CanvasGeneric
    {
        [SerializeField] private GameObject soundOffButton;
        [SerializeField] private GameObject hapticOffButton;



        public override void Initialize()
        {
            base.Initialize();
            ImageChecks();
        }

        private void ImageChecks()
        {
            var sound = PlayerPrefs.GetInt(Constants.SoundKey, 1);
            var haptic = PlayerPrefs.GetInt(Constants.HapticKey, 1);

            AudioListener.pause = sound == 0;

            soundOffButton.SetActive(sound == 0);
            hapticOffButton.SetActive(haptic == 0);
        }

        public void ToggleSound()
        {
            var sound = PlayerPrefs.GetInt(Constants.SoundKey, 1);
            PlayerPrefs.SetInt( Constants.SoundKey,sound == 1 ? 0 : 1);
            ImageChecks();
        }
        
        public void ToggleHaptic()
        {
            var haptic = PlayerPrefs.GetInt(Constants.HapticKey, 1);
            PlayerPrefs.SetInt( Constants.HapticKey,haptic == 1 ? 0 : 1);
            ImageChecks();
        }

        public void Exit()
        {
            CanvasManager.Instance.ShowMainMenuCanvas();
            CanvasManager.Instance.HidePauseMenuCanvas();
        }

        public void ChangeUserName()
        {
            CanvasManager.Instance.HidePauseMenuCanvas();
            CanvasManager.Instance.ShowMainMenuCanvas();
            
        }
        
    }
}