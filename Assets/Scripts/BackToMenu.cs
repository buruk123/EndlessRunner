using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MultiplayerScene")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PhotonNetwork.CloseConnection(player);
                }
            }
        }
        SceneManager.LoadScene("MainMenuScene");
    }
}
