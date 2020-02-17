using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private Button singlePlayerButton;
    [SerializeField] private Button multiPlayerButton;
    [SerializeField] private Button highscoreButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        singlePlayerButton.onClick.AddListener(LoadSingleGame);
        multiPlayerButton.onClick.AddListener(LoadMultiGame);
        highscoreButton.onClick.AddListener(ShowHighscores);
        exitButton.onClick.AddListener(ExitApplication);
    }

    private void LoadSingleGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void LoadMultiGame()
    {
        SceneManager.LoadScene("MultiplayerScene");
    }

    private void ShowHighscores()
    {
        SceneManager.LoadScene("HighscoresScene");
    }

    private void ExitApplication()
    {
        Application.Quit();
    }
}
