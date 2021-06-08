using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using DefaultNamespace;
using Enums;
using Skills;
using UI;
using UnityEngine;
using Photon.Pun;
using Random = System.Random;

namespace Managers
{
    public class SurroundingsManager : Singleton<SurroundingsManager>
    {
        [SerializeField] private Transform universeTransform;
        
        public GameObject CurrentEnvironment { get; private set; }
        public GameObject CurrentEnvironmentNetwork { get; private set; }

        public GameObject mainPlayer { get; private set; }
        public List<SkillMain> mainPlayerSkills = new List<SkillMain>();
        private List<GameObject> otherPlayers = new List<GameObject>();
        
        public delegate void PlayerChoosingDelegate();
        public event PlayerChoosingDelegate PlayerChoosingEvent;


        private void Awake()
        {
            GameManager.Instance.GoToMainMenuEvent += GoToMainMenu;
            RoundManager.Instance.StartRoundEvent += StartGame;
            GameManager.Instance.PlayerDiedEvent += Respawn;
        }

        private void StartGame()
        {
            Maps map = (Maps)PlayerPrefs.GetInt(Constants.MapKey, 0);

            if (mainPlayer)
            {
                PhotonNetwork.Destroy(mainPlayer);
            }
            foreach (var enemy in otherPlayers)
            {
                Destroy(enemy);
            }
            otherPlayers.Clear();

            CreateMainPlayer();

            //CreateOtherPlayers();
            
            LoadEnvironmentNetwork(map);
        }

        IEnumerator CreatePlayerDelayed()
        {
            yield return new WaitForSeconds(0.5f);
            CreateMainPlayer();
        }

        private void CreateMainPlayer()
        {
            Characters chosenCharacter = (Characters)PlayerPrefs.GetInt(Constants.PlayerKey, 0);
            var playerPrefab =
                Resources.Load(Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter)) as GameObject;
            string path = Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter);
            mainPlayer = PhotonNetwork.Instantiate(path, universeTransform.position, universeTransform.rotation);
            PhotonView.Get(this).RPC("setPlayerParent", RpcTarget.All, mainPlayer.GetComponent<PhotonView>().ViewID);
            //mainPlayer.transform.SetParent(universeTransform);
            var playerController =  mainPlayer.AddComponent<PlayerController>();

            foreach (var (skillMain,ix) in mainPlayerSkills.WithIndex())
            {
                playerController.AddSkill((SkillSlots)ix, skillMain.gameObject);
            }
            
            GameManager.Instance.PlayerSpawned();
        }

        [PunRPC]
        void setPlayerParent(int photonID)
        {
            PhotonView pv = PhotonView.Find(1);
            PhotonView player = PhotonView.Find(photonID);
            player.transform.SetParent(pv.transform);
        }

        public void CreatePlayerForChoosing(Characters chosenCharacter)
        {
            DestroyPlayerAfterChoosing();
            var playerPrefab =
                Resources.Load(Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter)) as GameObject;
            mainPlayer = Instantiate(playerPrefab, universeTransform);

            GameManager.Instance.PlayerSpawned();
            SpawnPositionRandom();
            mainPlayer.transform.LookAt(mainPlayer.transform.position - Vector3.forward);
            var animator = mainPlayer.GetComponentInChildren<Animator>();
            animator.SetBool(Constants.CharacterDanceBool,true);

            PlayerChoosingEvent?.Invoke();


        }
        

        public void DestroyPlayerAfterChoosing()
        {
            if (mainPlayer)
            {
                Destroy(mainPlayer);
            }
        }
        

        public void Respawn()
        {
            StartCoroutine(RespawnInit());
        }

        private IEnumerator RespawnInit()
        {
            CanvasManager.Instance.ShowDiedCanvas(Constants.RespawnTime+2);
            yield return new WaitForSeconds(Constants.RespawnTime+2);
            CanvasManager.Instance.HideDiedCanvas();
            if(RoundManager.Instance.roundState != RoundState.Game) yield break;
            if (mainPlayer)
            {
                Destroy(mainPlayer);
            }
            CreateMainPlayer();
            SpawnPositionRandom();
            InputManager.Instance.TakeInput();

        }

        private void CreateOtherPlayers()
        {
            for (int i = 0; i < Constants.PlayerCount-1; i++)
            {
                var chosenCharacter = (Characters) typeof(Characters).GetRandomEnumValue();
                var playerPrefab = Resources.Load(Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter)) as GameObject;
                var player = Instantiate(playerPrefab, universeTransform);
                //player.GetComponent<Rigidbody>().centerOfMass = new Vector3(0,-0.2f,0f);
                otherPlayers.Add(player);

            }
        }

        private void GoToMainMenu()
        {
            Maps map = (Maps)PlayerPrefs.GetInt(Constants.MapKey, 0);

            LoadEnvironment(map);
            mainPlayerSkills.Clear();
        }



        public void LoadEnvironment(Maps map)
        {

            if (CurrentEnvironment != null)
            {
                Destroy(CurrentEnvironment);
            }
            if (CurrentEnvironmentNetwork != null)
            {
                PhotonNetwork.Destroy(CurrentEnvironment);
            }

            GameObject envPref = Resources.Load(Constants.EnvironmentsPath + Enum.GetName(typeof(Maps),map)) as GameObject;
            CurrentEnvironment = Instantiate(envPref,universeTransform);
            RenderSettings.skybox = CurrentEnvironment.GetComponent<Map>().skyBox;

            SpawnPositionRandom();

            //for (int i = 0; i < otherPlayers.Count; i++)
            //{
            //    otherPlayers[i].transform.position = spawnPositions[i + 1].transform.position;
            //    otherPlayers[i].transform.LookAt(universeTransform.position);
            //    otherPlayers[i].transform.rotation = Quaternion.Euler(0,otherPlayers[i].transform.rotation.eulerAngles.y,0);}

        }


        public void LoadEnvironmentNetwork(Maps map)
        {
            if (CurrentEnvironment != null)
            {
                Destroy(CurrentEnvironment);
            }
            
            
            if (PhotonNetwork.IsMasterClient)
            {
                if (CurrentEnvironmentNetwork != null)
                {
                    PhotonNetwork.Destroy(CurrentEnvironmentNetwork);
                }
                GameObject envPref = Resources.Load(Constants.EnvironmentsPath + Enum.GetName(typeof(Maps),map)) as GameObject;
                CurrentEnvironmentNetwork = PhotonNetwork.Instantiate(Constants.EnvironmentsPath + Enum.GetName(typeof(Maps),map),universeTransform.position,envPref.transform.rotation);
                CurrentEnvironmentNetwork.GetComponent<PhotonView>().RPC("SetParent",RpcTarget.All,CurrentEnvironmentNetwork.GetComponent<PhotonView>().ViewID,1);
                RenderSettings.skybox = CurrentEnvironmentNetwork.GetComponent<Map>().skyBox;
            }
            
            SpawnPositionRandom();
        }
        
        

        private void SpawnPositionRandom()
        {
            if(mainPlayer == null) return;

            var spawnPositions = GameObject.FindGameObjectsWithTag(Constants.SpawnPositionTag);
            
            mainPlayer.transform.position = spawnPositions[0].transform.position;
        }
    }
}