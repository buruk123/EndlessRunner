using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMultiPlayer : MonoBehaviour
{
    public int Score => score;

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

            scoreText.GetComponent<Text>().text = "Score: " + score.ToString();
        }
        
    }
    public void SetGameState(bool isRunning)
    {
        isGameRunning = isRunning;
    }
}
