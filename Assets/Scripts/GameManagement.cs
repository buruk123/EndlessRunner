using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public BackgroundScroll BackgroundScroll;
    public PlayerMovement PlayerMovement;
    public EnemySpawn EnemySpawn;
    public UIManager UIManager;
    public Score score;
    public ScoreTextScript setScore;

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
        gameObject.SetActive(false);
        BackgroundScroll.SetGameState(true);
        PlayerMovement.SetGameState(true);
        EnemySpawn.SetGameState(true);
        score.SetGameState(true);
        UIManager.HidePanel();
    }

    public void GameOver()
    {
        isGameRunning = false;
        BackgroundScroll.SetGameState(false);
        PlayerMovement.SetGameState(false);
        EnemySpawn.SetGameState(false);
        score.SetGameState(false);
        setScore.ScoreText();
        UIManager.ShowEndGamePanel();
    }
}