using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}