using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Enums;
using Skills;
using UnityEngine;
using Random = System.Random;

namespace Managers
{
    public class SurroundingsManager : Singleton<SurroundingsManager>
    {
        [SerializeField] private Transform universeTransform;
        
        public GameObject CurrentEnvironment { get; private set; }

        public GameObject mainPlayer { get; private set; }
        private List<GameObject> otherPlayers = new List<GameObject>();
        

        private void Awake()
        {
            GameManager.Instance.GoToMainMenuEvent += GoToMainMenu;
            GameManager.Instance.StartGameEvent += StartGame;
        }

        private void StartGame()
        {
            Maps map = (Maps)PlayerPrefs.GetInt(Constants.MapKey, 0);

            if (mainPlayer != null)
            {
                Destroy(mainPlayer);
                foreach (var enemy in otherPlayers)
                {
                    Destroy(enemy);
                }
            }
            
            CreateMainPlayer();

            CreateOtherPlayers();
            
            LoadEnvironment(map);
        }

        private void CreateMainPlayer()
        {
            Characters chosenCharacter = (Characters)PlayerPrefs.GetInt(Constants.PlayerKey, 0);
            var playerPrefab =
                Resources.Load(Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter)) as GameObject;
            mainPlayer = Instantiate(playerPrefab, universeTransform);
            var playerController =  mainPlayer.AddComponent<PlayerController>();
            //mainPlayer.GetComponent<Rigidbody>().centerOfMass = new Vector3(0,-0.2f,0f);
            
            var skillObj  = Resources.Load(Constants.SkillDataPath + "FireProjectileSkill") as GameObject;

            playerController.AddSkill(SkillSlots.Base,skillObj);
            //playerController.AddSkill(SkillSlots.Extra1,playerController.gameObject.AddComponent<TeleportSkill>());
            //playerController.AddSkill(SkillSlots.Extra1,playerController.gameObject.AddComponent<ShieldSkill>());
            //playerController.AddSkill(SkillSlots.Extra2,playerController.gameObject.AddComponent<AOESkill>());
            
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
        }

        private void LoadEnvironment(Maps map)
        {
            if (CurrentEnvironment != null)
            {
                Destroy(CurrentEnvironment);
                
            }
            
            
            GameObject envPref = Resources.Load(Constants.EnvironmentsPath + Enum.GetName(typeof(Maps),map)) as GameObject;
            CurrentEnvironment = Instantiate(envPref, universeTransform);
            
            if(mainPlayer == null) return;
            
            var spawnPositions = GameObject.FindGameObjectsWithTag(Constants.SpawnPositionTag);
            
            mainPlayer.transform.position = spawnPositions[0].transform.position;

            for (int i = 0; i < otherPlayers.Count; i++)
            {
                otherPlayers[i].transform.position = spawnPositions[i + 1].transform.position;
                otherPlayers[i].transform.LookAt(universeTransform.position);
                otherPlayers[i].transform.rotation = Quaternion.Euler(0,otherPlayers[i].transform.rotation.eulerAngles.y,0);

            }

        }
    }
}