using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = .5f;
    public event Action<EnemyMovement> BeforeDestroy = delegate { };

    [SerializeField] private Rigidbody2D rb;
    private Vector2 screenBounds;

    private void Start()
    {
        rb.velocity = new Vector2(-speed, 0f);
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    private void Update()
    {
        if (transform.position.x < (-screenBounds.x * 2))
        {
            BeforeDestroy(this);
            Destroy(this.gameObject);
        }
    }

    public void GameOver()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
