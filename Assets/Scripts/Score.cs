﻿using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score;
    private float timer;
    private bool isGameRunning;

    private void Update()
    {
        if (isGameRunning)
        {
            timer += Time.deltaTime;
            if (timer > .1f)
            {
                score += 1;
                timer -= .1f;
            }
            scoreText.GetComponent<Text>().text = "Wynik: " + score.ToString();
        }
        
    }
    public void SetGameState(bool isRunning)
    {
        isGameRunning = isRunning;
    }
}
