using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button createRoom;
    [SerializeField] private Button joinRoom;
    [SerializeField] private InputField createNameOfRoom;
    [SerializeField] private InputField joinNameOfRoom;
    [SerializeField] private GameObject errorEmptyServerName;
    [SerializeField] private GameObject infoPlayerConnected;
    private const string VERSION = "v0.0.1";
    private string playerPrefabName = "Score";

    private void Start()
    {
        PhotonNetwork.GameVersion = VERSION;
        PhotonNetwork.ConnectUsingSettings();
        createRoom.onClick.AddListener(CreateRoom);
        joinRoom.onClick.AddListener(JoinRoom);
        Debug.Log("connecting to photon");
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
        Debug.Log("On Lobby Joined");
    }

    private async void CreateRoom()
    {
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
                IsVisible = false, MaxPlayers = 2
            };
            PhotonNetwork.CreateRoom(createNameOfRoom.text, options, TypedLobby.Default);
        }
        
    }

    private async void JoinRoom()
    {
        if (joinNameOfRoom.text == null)
        {
            errorEmptyServerName.SetActive(true);
            await Task.Delay(2000);
            errorEmptyServerName.SetActive(false);
        }
        else
        {
            PhotonNetwork.JoinRoom(joinNameOfRoom.text);
        }
    }
}
