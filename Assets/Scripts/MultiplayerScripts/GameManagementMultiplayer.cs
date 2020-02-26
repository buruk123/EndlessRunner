using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManagementMultiplayer : MonoBehaviour
{
    public BackgroundScroll BackgroundScroll;
    public PlayerMovement PlayerMovement;
    public EnemySpawn EnemySpawn;
    public UIManagerMultiplayer UIManager;
    public ScoreTextScriptMultiplayer ScoreMulti;
    [SerializeField] private Text countDown;
    [SerializeField] private Text playerScoreGameObject;
    private ScoreMultiplayer[] scores;
    private bool isGameRunning;
    private bool loadingGame;
    private float timeLeft = 5.0f;

    private void Start()
    {
        UIManager.ShowStartGamePanel();
        PlayerMovement.EnemyHit += GameOver;
        FindObjectOfType<AudioManager>().PlaySound("Background");
        loadingGame = true;
        isGameRunning = false;
    }

    private void Update()
    {
        if (PlayerMovement.gameOver) return;
        scores = FindObjectsOfType<ScoreMultiplayer>();

        if (isGameRunning == false)
        {
            isGameRunning = true;
            loadingGame = true;
        }

        if (isGameRunning && loadingGame)
        {
            timeLeft -= Time.deltaTime;
            countDown.text = timeLeft.ToString(CultureInfo.InvariantCulture).Substring(0, 1);
            if (timeLeft < 0)
            {
                ActivateGame();
                loadingGame = false;
            }
        }
    }

    private void ActivateGame()
    {
        BackgroundScroll.SetGameState(true);
        PlayerMovement.SetGameState(true);
        EnemySpawn.SetGameState(true);
        ScoreMulti.SetGameState(true);
        UIManager.HidePanel();
        PlayerMovement.gameOver = false;
        SetGameStateMultiplayer(true);
    }

    public void GameOver()
    {
        StartCoroutine(updateScore());
        isGameRunning = false;
        PlayerMovement.gameOver = true;
        BackgroundScroll.SetGameState(false);
        PlayerMovement.SetGameState(false);
        EnemySpawn.SetGameState(false);
        ScoreMulti.SetGameState(false);
        UIManager.ShowEndGamePanel();
        SetGameStateMultiplayer(false);

    }

    private IEnumerator updateScore()
    {
        var playerAndEnemyScores = playerScoreGameObject.text.Split('\n');
        var playerScore = playerAndEnemyScores[0].Substring(7);

        WWW site = new WWW("https://runnerendless.000webhostapp.com/uploadscore.php?score=" + playerScore + "&username=" + LoginManager.userName);
        Debug.Log("https://runnerendless.000webhostapp.com/uploadscore.php?score=" + playerScore + "&username=" + LoginManager.userName);
        yield return site;
    }

    private void SetGameStateMultiplayer(bool isRunning)
    {
        foreach (var scoreMultiplayer in scores)
        {
            scoreMultiplayer.SetGameState(isRunning);
        }
    }
}