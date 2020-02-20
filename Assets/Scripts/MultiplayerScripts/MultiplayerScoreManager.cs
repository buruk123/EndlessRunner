using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerScoreManager : MonoBehaviour
{
    private ArrayList allPlayers;
    private void Start()
    {
        allPlayers = new ArrayList();
        allPlayers.Add(PhotonNetwork.Instantiate("Score", Vector3.zero, Quaternion.identity));
    }
}
