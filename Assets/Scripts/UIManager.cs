using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private GameObject startGameContainer;
    [SerializeField] private GameObject endGameContainer;
    [SerializeField] private GameObject scoreContainer;

    public void ShowStartGamePanel()
    {
        uiContainer.SetActive(true);
        startGameContainer.SetActive(true);
        endGameContainer.SetActive(false);
        scoreContainer.SetActive(false);
    }

    public void ShowEndGamePanel()
    {
        uiContainer.SetActive(true);
        startGameContainer.SetActive(false);
        endGameContainer.SetActive(true);
        scoreContainer.SetActive(false);
    }

    public void HidePanel()
    {
        uiContainer.SetActive(false);
        startGameContainer.SetActive(false);
        endGameContainer.SetActive(false);
        scoreContainer.SetActive(true);
    }
}