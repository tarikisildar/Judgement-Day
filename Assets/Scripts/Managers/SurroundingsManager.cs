using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Enums;
using Skills;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Managers
{
    public class SurroundingsManager : Singleton<SurroundingsManager>
    {
        [SerializeField] private Transform universeTransform;
        
        public GameObject CurrentEnvironment { get; private set; }

        public GameObject mainPlayer { get; private set; }
        public List<SkillMain> mainPlayerSkills = new List<SkillMain>();
        private List<GameObject> otherPlayers = new List<GameObject>();
        

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
                Destroy(mainPlayer);
            }
            foreach (var enemy in otherPlayers)
            {
                Destroy(enemy);
            }
            otherPlayers.Clear();
            
            CreateMainPlayer();

            //CreateOtherPlayers();
            
            LoadEnvironment(map);
        }

        private void CreateMainPlayer()
        {
            Characters chosenCharacter = (Characters)PlayerPrefs.GetInt(Constants.PlayerKey, 0);
            var playerPrefab =
                Resources.Load(Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter)) as GameObject;
            mainPlayer = Instantiate(playerPrefab, universeTransform);
            var playerController =  mainPlayer.AddComponent<PlayerController>();

            foreach (var (skillMain,ix) in mainPlayerSkills.WithIndex())
            {
                playerController.AddSkill((SkillSlots)ix, skillMain.gameObject);
            }
            
            GameManager.Instance.PlayerSpawned();
            //mainPlayer.GetComponent<Rigidbody>().centerOfMass = new Vector3(0,-0.2f,0f);
            
            //var skillObj  = Resources.Load(Constants.SkillDataPath + "FireProjectileSkill") as GameObject;

            //playerController.AddSkill(SkillSlots.Base,skillObj);
            //playerController.AddSkill(SkillSlots.Extra1,playerController.gameObject.AddComponent<TeleportSkill>());
            //playerController.AddSkill(SkillSlots.Extra1,playerController.gameObject.AddComponent<ShieldSkill>());
            //playerController.AddSkill(SkillSlots.Extra2,playerController.gameObject.AddComponent<AOESkill>());
            
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
            LoadEnvironment(Maps.Standard);
            mainPlayerSkills.Clear();
        }

        private void LoadEnvironment(Maps map)
        {
            if (CurrentEnvironment != null)
            {
                Destroy(CurrentEnvironment);
            }
            
            
            GameObject envPref = Resources.Load(Constants.EnvironmentsPath + Enum.GetName(typeof(Maps),map)) as GameObject;
            CurrentEnvironment = Instantiate(envPref, universeTransform);
            
            SpawnPositionRandom();

            //for (int i = 0; i < otherPlayers.Count; i++)
            //{
            //    otherPlayers[i].transform.position = spawnPositions[i + 1].transform.position;
            //    otherPlayers[i].transform.LookAt(universeTransform.position);
            //    otherPlayers[i].transform.rotation = Quaternion.Euler(0,otherPlayers[i].transform.rotation.eulerAngles.y,0);}

        }

        private void SpawnPositionRandom()
        {
            if(mainPlayer == null) return;

            var spawnPositions = GameObject.FindGameObjectsWithTag(Constants.SpawnPositionTag);
            
            mainPlayer.transform.position = spawnPositions[0].transform.position;
        }
    }
}