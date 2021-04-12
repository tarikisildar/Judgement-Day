using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
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
            Characters chosenCharacter = (Characters)PlayerPrefs.GetInt(Constants.PlayerKey, 0);
            var playerPrefab =
                Resources.Load(Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter)) as GameObject;
            mainPlayer = Instantiate(playerPrefab, universeTransform);

            for (int i = 0; i < Constants.PlayerCount-1; i++)
            {
                chosenCharacter = (Characters) typeof(Characters).GetRandomEnumValue();
                playerPrefab = Resources.Load(Constants.CharactersPath + Enum.GetName(typeof(Characters), chosenCharacter)) as GameObject;
                otherPlayers.Add(Instantiate(playerPrefab, universeTransform));
            }
            
            LoadEnvironment(map);
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
            }

        }
    }
}