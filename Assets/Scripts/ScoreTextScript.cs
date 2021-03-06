﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    private string scoreText;
    [SerializeField] private Score score;
    [SerializeField] private ScoreTextScript setScore;


    public int ScoreText()
    {
        scoreText = score.GetComponent<Text>().text;
        setScore.GetComponent<Text>().text = "Gratulacje\nZdobyłeś " + scoreText.Substring(7) + " punktów!";
        return int.Parse(scoreText.Substring(7));
    }
}