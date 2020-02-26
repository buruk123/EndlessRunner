using System.Collections;
using System.Globalization;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public BackgroundScroll BackgroundScroll;
    public PlayerMovement PlayerMovement;
    public EnemySpawn EnemySpawn;
    public UIManager UIManager;
    public Score score;
    public ScoreTextScript setScore;
    [SerializeField] private Text countDown;
    private bool isGameRunning;
    private bool loadingGame;
    private float timeLeft = 5.0f;

    private void Start()
    {
        UIManager.ShowStartGamePanel();
        PlayerMovement.EnemyHit += GameOver;
        FindObjectOfType<AudioManager>().PlaySound("Background");
        isGameRunning = false;
        loadingGame = false;
    }

    private void Update()
    {
        if (PlayerMovement.gameOver) return;
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
        //gameObject.SetActive(false);
        BackgroundScroll.SetGameState(true);
        EnemySpawn.SetGameState(true);
        score.SetGameState(true);
        UIManager.HidePanel();
        PlayerMovement.SetGameState(true);
    }

    public void GameOver()
    {
        StartCoroutine(updateScore());
        isGameRunning = false;
        BackgroundScroll.SetGameState(false);
        PlayerMovement.SetGameState(false);
        EnemySpawn.SetGameState(false);
        score.SetGameState(false);
        setScore.ScoreText();
        UIManager.ShowEndGamePanel();
        
    }

    private IEnumerator updateScore()
    {
        WWW site = new WWW("https://runnerendless.000webhostapp.com/uploadscore.php?score=" + setScore.ScoreText() + "&username=" + LoginManager.userName);
        yield return site;
    }
}