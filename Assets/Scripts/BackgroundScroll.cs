using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float bgSpeed;
    private bool isGameRunning;
    public Renderer bgRenderer;

    private void Update()
    {
        if (!isGameRunning) return;
        bgRenderer.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, 0f);
    }

    public void SetGameState(bool isRunning)
    {
        isGameRunning = isRunning;
    }
}