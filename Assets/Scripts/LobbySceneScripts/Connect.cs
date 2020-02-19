using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connect : NetworkManager
{
    [SerializeField] private Button startHost;
    [SerializeField] private Button joinHost;
    [SerializeField] private InputField networkNameToJoin;
    [SerializeField] private GameObject errorEmptyServerName;
    [SerializeField] private GameObject infoClientConnected;
    public NetworkManager manager;
    public NetworkClient client;
    private bool playerConnected;

    public event Action PlayerHasConnected = delegate { };

    public bool PlayerConnected
    {
        get { return playerConnected; }
        set
        {
            playerConnected = value;
            if (playerConnected)
            {
                PlayerHasConnected();
            }
        }
    }

    private void Start()
    {
        manager = FindObjectOfType<NetworkManager>();
        startHost.onClick.AddListener(StartHosting);
        joinHost.onClick.AddListener(JoinToGame);
        PlayerHasConnected += OnPlayerHasConnected;
    }

    private async void OnPlayerHasConnected()
    {
        infoClientConnected.SetActive(true);
        await Task.Delay(2000);
        infoClientConnected.SetActive(false);
        SceneManager.LoadScene("MultiplayerScene");
    }

    private async void StartHosting()
    {
        manager.networkPort = 7777;
        client = manager.StartHost();
        startHost.onClick.RemoveAllListeners();
        startHost.onClick.AddListener(StopHosting);
        startHost.GetComponentInChildren<Text>().text = "StopHost";
    }

    private void StopHosting()
    {
        if (!manager.isNetworkActive) return;
        manager.StopHost();
        client = null;
        startHost.onClick.RemoveAllListeners();
        startHost.onClick.AddListener(StartHosting);
        startHost.GetComponentInChildren<Text>().text = "StartHost";
    }

    private async void JoinToGame()
    {
        if (networkNameToJoin.text == null)
        {
            errorEmptyServerName.SetActive(true);
            await Task.Delay(2000);
            errorEmptyServerName.SetActive(false);
        }
        else
        {
            manager.networkAddress = networkNameToJoin.text;
            manager.networkPort = 7777;
            client = manager.StartClient();
            await Task.Delay(1000);
            PlayerConnected = client.isConnected;

        }
    }

    public override async void OnServerConnect(NetworkConnection conn)
    {
        if (NetworkServer.connections.Count != 2) return;
        await Task.Delay(1000);
        infoClientConnected.SetActive(true);
        await Task.Delay(2000);
        infoClientConnected.SetActive(false);
        SceneManager.LoadScene("MultiplayerScene");

    }
}
