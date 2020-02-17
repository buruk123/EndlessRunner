using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHighscores : MonoBehaviour
{
    [SerializeField] private Text name;
    [SerializeField] private Text score;
    private IEnumerator Start()
    {
        WWW highscoresStoredURL = new WWW("https://runnerendless.000webhostapp.com/highscores.php");
        yield return highscoresStoredURL;
        string text = highscoresStoredURL.text;
        string[] allHighscoreData = text.Split('|');
        string[] names = new string[allHighscoreData.Length], points = new string[allHighscoreData.Length];
        for (int i = 0; i < allHighscoreData.Length - 1; i++)
        {
            string[] data = allHighscoreData[i].Split(' ');
            names[i] = data[0];
            points[i] = data[1];
        }

        for(int i = 0; i < names.Length; i++)
        {
            name.text += names[i] + "\n\n";
            score.text += points[i] + "\n\n";
        }
        
    }

}
