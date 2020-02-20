using System.Threading;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMultiplayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private Text scoreText;
    [SerializeField] private int playerScore;
    [SerializeField] private int enemyScore;
    private PhotonView photonView;

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
        if (isGameRunning)
        {
            timer += Time.deltaTime;
            if (timer > .1f)
            {
                playerScore += 1;
                timer -= .1f;
            }
            scoreText.GetComponent<Text>().text = "Score: " + playerScore;
            // Debug.Log("Playerscore: " + playerScore + "||| Enemyscore: " + enemyScore);
        }
        
    }
    public void SetGameState(bool isRunning)
    {
        isGameRunning = isRunning;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("henlo");
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
