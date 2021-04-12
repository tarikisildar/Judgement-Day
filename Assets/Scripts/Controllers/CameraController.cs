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
        private CinemachineBrain brain;

        private void Awake()
        {
            brain = GetComponent<CinemachineBrain>();
            GameManager.Instance.GoToMainMenuEvent += LockEmpty;
            GameManager.Instance.StartGameEvent += LockOnPlayer;

        }

        private void LockOnPlayer()
        {
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
            playerCam.Priority = 9;
        }
        
    }
}