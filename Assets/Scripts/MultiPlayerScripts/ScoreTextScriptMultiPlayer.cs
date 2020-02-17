using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScriptMultiPlayer : MonoBehaviour
{
    private string scoreText;
    [SerializeField] private ScoreMultiPlayer score;
    [SerializeField] private ScoreTextScriptMultiPlayer setScore;


    public int ScoreText()
    {
        scoreText = score.GetComponent<Text>().text;
        setScore.GetComponent<Text>().text = "Gratulacje\nZdobyłeś " + scoreText.Substring(7) + " punktów!";
        return int.Parse(scoreText.Substring(7));
    }
}