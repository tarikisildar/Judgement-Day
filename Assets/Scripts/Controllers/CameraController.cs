using System;
using System.Collections;
using Cinemachine;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera playerCam;
        [SerializeField] private CinemachineVirtualCamera emptyCam;
        [SerializeField] private CinemachineVirtualCamera watchGameCam;
        [SerializeField] private CinemachineVirtualCamera playerCamZoom;
        private CinemachineBrain brain;

        private void Awake()
        {
            brain = GetComponent<CinemachineBrain>();
            RoundManager.Instance.EndRoundEvent += LockEmpty;
            GameManager.Instance.GoToMainMenuEvent += LockEmpty;
            GameManager.Instance.PlayerSpawnedEvent += LockOnPlayer;
            GameManager.Instance.PlayerDiedEvent += WatchGame;
            SurroundingsManager.Instance.PlayerChoosingEvent += LockOnZoom;
            
        }

        private void LockOnZoom()
        {
            NormalPriority();
            playerCamZoom.Priority = 11;
            playerCamZoom.Follow = SurroundingsManager.Instance.mainPlayer.transform;
            playerCamZoom.LookAt = SurroundingsManager.Instance.mainPlayer.transform;

        }
        private void LockOnPlayer()
        {
            NormalPriority();

            playerCam.Priority = 11;

            StartCoroutine(WaitForLocking());
        }

        IEnumerator WaitForLocking()
        {
            yield return new WaitForSeconds(Constants.FirstBatch);
            playerCam.Follow = SurroundingsManager.Instance.mainPlayer.transform;

        }
        
        private void LockEmpty()
        {
            NormalPriority();
            emptyCam.Priority = 11;

        }

        private void WatchGame()
        {
            NormalPriority();
            watchGameCam.Priority = 11;
            
        }

        private void NormalPriority()
        {
            watchGameCam.Priority = 10;
            playerCam.Priority = 10;
            emptyCam.Priority = 10;
            playerCamZoom.Priority = 10;
        }
    }
}