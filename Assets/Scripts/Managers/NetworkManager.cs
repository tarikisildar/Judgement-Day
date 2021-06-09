using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Managers;
using Photon.Pun.UtilityScripts;
using UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static bool gameReady = false;
    public static List<GameObject> players = new List<GameObject>();
    private SearchRoomPopup searchRoomPopup;

    void Start()
    {
        if(PlayerPrefs.HasKey(Constants.UserNameKey))
        {
            SetUpName();
        }

        searchRoomPopup = CanvasManager.Instance.GetPopUpCanvas().GetSearchRoomPopUp();

    }

    
    public static void FindRoom()
    {
        if(PhotonNetwork.IsConnected)
        {
            ExitGames.Client.Photon.Hashtable customPropreties = new ExitGames.Client.Photon.Hashtable();
            customPropreties["Map"] = PlayerPrefs.GetInt(Constants.MapKey, 0);
            PhotonNetwork.JoinRandomRoom(customPropreties,0);
        }
        else
        {
            PhotonNetwork.GameVersion = Constants.GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public static void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    public static void Connect()
    {
        PhotonNetwork.GameVersion = Constants.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetUpName()
    {
        if(!PlayerPrefs.HasKey(Constants.UserNameKey)) { return; }

        string playerName = PlayerPrefs.GetString(Constants.UserNameKey);
        PhotonNetwork.NickName = playerName;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server.");
        CanvasManager.Instance.GetPopUpCanvas().HideConnectingPopUp();
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected from server. Photon log: {cause}");
        CanvasManager.Instance.ShowPopUpCanvas();
        CanvasManager.Instance.GetPopUpCanvas().ShowConnectingPopUp();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found, creating a new room.");
        searchRoomPopup.WaitingPlayers();
        searchRoomPopup.SetUserNames();

       // PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = (byte)Constants.MaxPlayersPerRoom });
        
        ExitGames.Client.Photon.Hashtable customPropreties = new ExitGames.Client.Photon.Hashtable();
        customPropreties["Map"] = PlayerPrefs.GetInt(Constants.MapKey, 0);
 
        RoomOptions roomOptions = new RoomOptions() {CustomRoomProperties = customPropreties, IsVisible = true, IsOpen = true, MaxPlayers = (byte)Constants.MaxPlayersPerRoom};
 
        roomOptions.CustomRoomPropertiesForLobby = new string[]
        {
            "Map",
        };
   
        PhotonNetwork.CreateRoom("Room" + Random.Range(0, 1000), roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined a room.");
        searchRoomPopup.WaitingPlayers();
        searchRoomPopup.SetUserNames();

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if(playerCount != Constants.MaxPlayersPerRoom)
        {
            Debug.Log("Waiting for opponents.");
        }
        else
        {
            Debug.Log("Match is ready to begin.");
            gameReady = true;
            StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        searchRoomPopup.SetUserNames();
        if(PhotonNetwork.CurrentRoom.PlayerCount == Constants.MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("Match is ready to begin.");
            gameReady = true;
            StartGame();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        searchRoomPopup.SetUserNames();

    }

    public static void Disconnect()
    {
        PhotonNetwork.DestroyAll();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    void StartGame()
    {
        searchRoomPopup.Starting();
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        CanvasManager.Instance.GetPopUpCanvas().HideSearchRoomPopUp();

        GameManager.Instance.StartGame();
    }
}
