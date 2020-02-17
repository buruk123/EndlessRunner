using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartScene()
    {
        if (SceneManager.GetSceneByName("SampleScene").isLoaded)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            SceneManager.LoadScene("MultiplayerScene");
        }
        
    }
}