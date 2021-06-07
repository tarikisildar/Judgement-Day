using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Managers;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static bool gameReady = false;
    public static List<GameObject> players = new List<GameObject>();

    void Start()
    {
        if(PlayerPrefs.HasKey(Constants.PlayerKey))
        {
            SetUpName();
        }
    }

    
    public static void FindRoom()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = Constants.GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void SetUpName()
    {
        if(!PlayerPrefs.HasKey(Constants.PlayerKey)) { return; }

        string playerName = PlayerPrefs.GetString(Constants.PlayerKey);

        PhotonNetwork.NickName = playerName;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server.");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected from server. Photon log: {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found, creating a new room.");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = (byte)Constants.MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined a room.");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if(playerCount != Constants.MaxPlayersPerRoom)
        {
            Debug.Log("Waiting for opponents.");
        }
        else
        {
            Debug.Log("Match is ready to begin.");
            gameReady = true;
            StartCoroutine(CountDown());
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == Constants.MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("Match is ready to begin.");
            gameReady = true;
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.StartGame();
    }
}
