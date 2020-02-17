using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private GameObject startGameContainer;
    [SerializeField] private GameObject endGameContainer;
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private GameObject enemyScoreContainer;

    public void ShowStartGamePanel()
    {
        uiContainer.SetActive(true);
        startGameContainer.SetActive(true);
        endGameContainer.SetActive(false);
        scoreContainer.SetActive(false);
        enemyScoreContainer.SetActive(false);
    }

    public void ShowEndGamePanel()
    {
        uiContainer.SetActive(true);
        startGameContainer.SetActive(false);
        endGameContainer.SetActive(true);
        scoreContainer.SetActive(false);
        enemyScoreContainer.SetActive(false);
    }

    public void HidePanel()
    {
        uiContainer.SetActive(false);
        startGameContainer.SetActive(false);
        endGameContainer.SetActive(false);
        scoreContainer.SetActive(true);
        enemyScoreContainer.SetActive(true);
    }
}