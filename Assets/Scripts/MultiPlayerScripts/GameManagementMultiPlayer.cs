using System.Collections;
using UnityEngine;

public class GameManagementMultiPlayer : MonoBehaviour
{
    public BackgroundScroll BackgroundScroll;
    public PlayerMovement PlayerMovement;
    public EnemySpawn EnemySpawn;
    public UIManagerMultiPlayer UIManager;
    public ScoreMultiPlayer Score;
    public ScoreTextScriptMultiPlayer SetScore;
    public EnemyScoreMultiPlayer EnemyScore;

    private bool isGameRunning;

    private void Start()
    {
        UIManager.ShowStartGamePanel();
        PlayerMovement.EnemyHit += GameOver;
        FindObjectOfType<AudioManager>().PlaySound("Background");
    }

    private void Update()
    {
        if (isGameRunning == false && Input.GetKey(KeyCode.Return))
        {
            isGameRunning = true;
            ActivateGame();
        }
    }

    private void ActivateGame()
    {
        //gameObject.SetActive(false);
        BackgroundScroll.SetGameState(true);
        PlayerMovement.SetGameState(true);
        EnemySpawn.SetGameState(true);
        Score.SetGameState(true);
        EnemyScore.SetGameState(true);
        UIManager.HidePanel();
    }

    public void GameOver()
    {
        StartCoroutine(updateScore());
        isGameRunning = false;
        BackgroundScroll.SetGameState(false);
        PlayerMovement.SetGameState(false);
        EnemySpawn.SetGameState(false);
        Score.SetGameState(false);
        EnemyScore.SetGameState(false);
        SetScore.ScoreText();
        UIManager.ShowEndGamePanel();
        
    }

    private IEnumerator updateScore()
    {
        WWW site = new WWW("https://runnerendless.000webhostapp.com/uploadscore.php?score=" + SetScore.ScoreText() + "&username=" + LoginManager.userName);
        yield return site;
    }
}