using System;
using DG.Tweening.Plugins;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScriptMultiplayer : MonoBehaviour
{
    private string scoreText;
    [SerializeField] private Text score;
    private bool isRunning;
    private string[] splittedText;
    private string playerScore;
    private string enemyScore;

    private void Update()
    {
        if (!isRunning)
        {
            ScoreText();
        }
    }

    public void ScoreText()
    {
        scoreText = score.text;

        splittedText = scoreText.Split('\n');
        playerScore = splittedText[0].Substring(7);
        enemyScore = splittedText[1].Substring(19);

        GetComponent<Text>().text = "Gratulacje!\nZdobyłeś " + playerScore + " punktów!\nTwój przeciwnik uzyskał " + enemyScore + " punktów.";
    }

    public void SetGameState(bool isGameRunning)
    {
        isRunning = isGameRunning;
    }
}