using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public float respawnTime = 1.5f;
    private bool isGameRunning;
    private Vector2 screenBounds;

    private List<EnemyMovement> enemies = new List<EnemyMovement>();
    private Coroutine enemySpawner;

    private void Start()
    {
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void SetGameState(bool isRunning)
    {
        isGameRunning = isRunning;
        if (isRunning)
        {
            enemySpawner = StartCoroutine(spawnEnumerator());
        }
        else
        {
            StopCoroutine(enemySpawner);
            foreach (var enemyMovement in enemies)
            {
                enemyMovement.GameOver();
            }
        }
    }

    private void SpawnEnemy()
    {
        var enemyObject = Instantiate(enemy);
        var enemyMovementScript = enemyObject.GetComponent<EnemyMovement>();
        enemyMovementScript.BeforeDestroy += OnBeforeDestroyEnemy;
        enemies.Add(enemyMovementScript);
        enemyObject.transform.position = new Vector3(screenBounds.x * 2, Random.Range(-3f, 4f));
    }

    private void OnBeforeDestroyEnemy(EnemyMovement enemy)
    {
        enemies.Remove(enemy);
        enemy.BeforeDestroy -= OnBeforeDestroyEnemy;

        //chyba dziala
        //a da sie sfreezowac pozycje gracza na gameover?
        //chyba tak daj chwile
    }

    private IEnumerator spawnEnumerator()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
    }
}