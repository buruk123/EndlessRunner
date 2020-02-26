using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    private PhotonView photonView;
    private string playerName;
    private const string PLAYER_PREFAB_NAME = "Score";

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Debug.Log(photonView);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MultiplayerScene")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }

    private void MasterLoadedGame()
    {
        // photonView.RPC("RPC_LoadGameScene", RpcTarget.MasterClient);
        photonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }

    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadGameScene", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel("MultiplayerScene");
    }

    [PunRPC]
    private void RPC_LoadGameScene()
    {
        Debug.Log("Ilosc graczy: " + PhotonNetwork.PlayerList.Length);
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            Debug.Log("all players are in game");
            photonView.RPC("CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(PLAYER_PREFAB_NAME, Vector3.zero, Quaternion.identity, 0);
    }
}
