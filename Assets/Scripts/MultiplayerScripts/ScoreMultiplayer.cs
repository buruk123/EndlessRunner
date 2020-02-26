using System.Threading;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMultiplayer : MonoBehaviourPun, IPunObservable
{
    public int playerScore;
    public int enemyScore;
    private PhotonView photonView;
    private Text scoreText;
    private float timer;
    private bool isGameRunning;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
    }

    private void Update()
    {
        scoreText = GameObject.Find("CanvasWithScore").GetComponentInChildren<Text>();
        if (isGameRunning)
        {
            timer += Time.deltaTime;
            if (timer > .1f)
            {
                playerScore += 1;
                timer -= .1f;
            }
        }
        if (enemyScore != 0)
        {
            scoreText.text = "Wynik: " + playerScore + "\nWynik przeciwnika: " + enemyScore;
        }
        

    }
    public void SetGameState(bool isRunning)
    {
        isGameRunning = isRunning;
        // scoreText.gameObject.SetActive(isRunning);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Debug.Log("IsPlayerPlaying " + isPlayerPlaying + "|||IsEnemyPlaying " + isEnemyPlaying);
        if (stream.IsWriting)
        {
            stream.SendNext(playerScore);
        }
        else
        {
            enemyScore = (int) stream.ReceiveNext();
        }
    }
}
