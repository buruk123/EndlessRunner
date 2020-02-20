using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;
    [Header("Player Object")]
    [SerializeField] private string playerPrefabName;
    [SerializeField] private string playerName;

    [Header("Others")]
    public ArrayList photonPlayers;
    [SerializeField] private Button createRoom;
    [SerializeField] private Button joinRoom;
    [SerializeField] private InputField createNameOfRoom;
    [SerializeField] private InputField joinNameOfRoom;
    [SerializeField] private GameObject errorEmptyServerName;
    [SerializeField] private GameObject infoPlayerConnected;
    [SerializeField] private GameObject errorOnJoinRoomFailed;
    [SerializeField] private GameObject errorOnCreateRoomFailed;
    [SerializeField] private GameObject errorOnRoomDeleted;
    private const string VERSION = "v0.0.1";

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = VERSION;
        PhotonNetwork.ConnectUsingSettings();
        createRoom.interactable = false;
        joinRoom.interactable = false;
        Debug.Log("connecting to server");
    }

    

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("connected");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("joined lobby");
        createRoom.interactable = true;
        joinRoom.interactable = true;
        createRoom.onClick.RemoveAllListeners();
        joinRoom.onClick.RemoveAllListeners();
        createRoom.onClick.AddListener(delegate { ChangeRoomCreatedOrNot(createRoom); });
        joinRoom.onClick.AddListener(delegate { JoinRoomOrShowJoined(joinRoom); });
        
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("room created");
    }

    public override async void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        newPlayer.NickName = "adsad";
        Debug.Log("player entered");
        infoPlayerConnected.SetActive(true);
        await Task.Delay(5000);
        infoPlayerConnected.SetActive(false);
        // PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity);
        PhotonNetwork.LoadLevel("MultiplayerScene");
    }

    public override async void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("joined room");
        if (PhotonNetwork.PlayerList.Length != 2) return;
        infoPlayerConnected.SetActive(true);
        await Task.Delay(5000);
        infoPlayerConnected.SetActive(false);
        // PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity);
        PhotonNetwork.LoadLevel("MultiplayerScene");
    }

    public override async void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        joinRoom.GetComponentInChildren<Text>().text = "Dołącz do pokoju";
        errorOnRoomDeleted.SetActive(true);
        await Task.Delay(2000);
        errorOnRoomDeleted.SetActive(false);

    }

    public override async void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        errorOnJoinRoomFailed.SetActive(true);
        await Task.Delay(2000);
        errorOnJoinRoomFailed.SetActive(false);
    }

    public override async void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        errorOnCreateRoomFailed.SetActive(true);
        await Task.Delay(2000);
        errorOnCreateRoomFailed.SetActive(false);
    }

    private async void ChangeRoomCreatedOrNot(Button button)
    {
        string buttonName = button.GetComponentInChildren<Text>().text;
        Debug.Log(buttonName);
        switch (buttonName)
        {
            case "Zamknij pokój":
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PhotonNetwork.CloseConnection(player);
                }
                button.GetComponentInChildren<Text>().text = "Stwórz pokój";
                break;
            case "Stwórz pokój":
                Debug.Log("siema");
                if (createNameOfRoom.text == null)
                {
                    errorEmptyServerName.SetActive(true);
                    await Task.Delay(2000);
                    errorEmptyServerName.SetActive(false);
                }
                else
                {
                    RoomOptions options = new RoomOptions()
                    {
                        IsVisible = false,
                        MaxPlayers = 2
                    };
                    PhotonNetwork.CreateRoom(createNameOfRoom.text, options, TypedLobby.Default);
                    button.GetComponentInChildren<Text>().text = "Zamknij pokój";
                }
                break;
        }
    }

    private async void JoinRoomOrShowJoined(Button button)
    {
        string buttonName = button.GetComponentInChildren<Text>().text;
        switch (buttonName)
        {
            case "Dołącz do pokoju":
                if (joinNameOfRoom.text == null)
                {
                    errorEmptyServerName.SetActive(true);
                    await Task.Delay(2000);
                    errorEmptyServerName.SetActive(false);
                }
                else
                {
                    PhotonNetwork.JoinRoom(joinNameOfRoom.text);
                    button.GetComponentInChildren<Text>().text = "Wyjdź z pokoju";
                }
                break;
            case "Wyjdź z pokoju":
                PhotonNetwork.LeaveRoom();
                button.GetComponentInChildren<Text>().text = "Dołącz do pokoju";
                break;
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity, 0);
    }
}