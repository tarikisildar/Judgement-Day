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
        private CinemachineBrain brain;

        private void Awake()
        {
            brain = GetComponent<CinemachineBrain>();
            RoundManager.Instance.EndRoundEvent += LockEmpty;
            GameManager.Instance.GoToMainMenuEvent += LockEmpty;
            GameManager.Instance.PlayerSpawnedEvent += LockOnPlayer;
            GameManager.Instance.PlayerDiedEvent += WatchGame;
        }

        private void LockOnPlayer()
        {
            playerCam.Priority = 11;
            watchGameCam.Priority = 10;
            emptyCam.Priority = 10;

            StartCoroutine(WaitForLocking());
        }

        IEnumerator WaitForLocking()
        {
            yield return new WaitForSeconds(Constants.FirstBatch);
            playerCam.Follow = SurroundingsManager.Instance.mainPlayer.transform;

        }
        
        private void LockEmpty()
        {
            playerCam.Priority = 10;
            emptyCam.Priority = 11;
            watchGameCam.Priority = 10;

        }

        private void WatchGame()
        {
            watchGameCam.Priority = 11;
            playerCam.Priority = 10;
            emptyCam.Priority = 10;
        }
        
    }
}