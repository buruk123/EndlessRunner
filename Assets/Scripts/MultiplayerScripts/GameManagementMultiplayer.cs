using System.Collections;
using Photon.Pun;
using UnityEngine;

public class GameManagementMultiplayer : MonoBehaviour
{
    public BackgroundScroll BackgroundScroll;
    public PlayerMovement PlayerMovement;
    public EnemySpawn EnemySpawn;
    public UIManagerMultiplayer UIManager;
    // public ScoreTextScript setScore;
    private ScoreMultiplayer[] scores;
    private bool isGameRunning;

    private void Start()
    {
        UIManager.ShowStartGamePanel();
        PlayerMovement.EnemyHit += GameOver;
        FindObjectOfType<AudioManager>().PlaySound("Background");
    }

    private void Update()
    {
        scores = FindObjectsOfType<ScoreMultiplayer>();
        if (isGameRunning == false && (Input.GetKey(KeyCode.Return) || Input.touchCount > 0))
        {
            isGameRunning = true;
            ActivateGame();
            foreach (var scoreMultiplayer in scores)
            {
                scoreMultiplayer.SetGameState(isGameRunning);
            }

        }
    }

    private void ActivateGame()
    {
        //gameObject.SetActive(false);
        BackgroundScroll.SetGameState(true);
        PlayerMovement.SetGameState(true);
        EnemySpawn.SetGameState(true);
        // score.SetGameState(true);
        UIManager.HidePanel();
    }

    public void GameOver()
    {
        // StartCoroutine(updateScore());
        isGameRunning = false;
        BackgroundScroll.SetGameState(false);
        PlayerMovement.SetGameState(false);
        EnemySpawn.SetGameState(false);
        // score.SetGameState(false);
        // setScore.ScoreText();
        UIManager.ShowEndGamePanel();
        
    }

    // private IEnumerator updateScore()
    // {
    //     WWW site = new WWW("https://runnerendless.000webhostapp.com/uploadscore.php?score=" + setScore.ScoreText() + "&username=" + LoginManager.userName);
    //     yield return site;
    // }
}