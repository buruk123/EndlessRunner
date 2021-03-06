﻿using UnityEngine;

public class UIManagerMultiplayer : MonoBehaviour
{
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private GameObject canvasWithScore;
    [SerializeField] private GameObject startGameContainer;
    [SerializeField] private GameObject endGameContainer;

    public void ShowStartGamePanel()
    {
        uiContainer.SetActive(true);
        canvasWithScore.SetActive(true);
        startGameContainer.SetActive(true);
        endGameContainer.SetActive(false);
    }

    public void ShowEndGamePanel()
    {
        uiContainer.SetActive(true);
        canvasWithScore.SetActive(true);
        startGameContainer.SetActive(false);
        endGameContainer.SetActive(true);
    }

    public void HidePanel()
    {
        uiContainer.SetActive(false);
        canvasWithScore.SetActive(true);
        startGameContainer.SetActive(false);
        endGameContainer.SetActive(false);
    }
}